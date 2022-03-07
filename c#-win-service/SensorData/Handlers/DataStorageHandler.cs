﻿using Constants;
using Interfaces;
using Services.DbService;
using Services.FileService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Handlers
{
    public class DataStorageHandler : IDataHandler
    {
        #region Constructor
        public DataStorageHandler() { }
        #endregion

        #region IDataHandler
        /// <summary>
        /// Dynamically invokes the appropriate method based on the configured storage method.
        /// </summary>
        /// <param name="measurements">The measurements to be saved.</param>
        public Dictionary<Type, List<IMeasurement>> HandleData(Dictionary<Type, List<IMeasurement>> measurements)
        {
            StorageMethod activeStorageMethod = GetActiveStorageMethod();
            List<StorageMethod> configuredMethods = GetConfiguredStorageMethods();
            if (ValidateStorageMethod(activeStorageMethod, configuredMethods))
            {
                MethodInfo methodInfo = typeof(DataStorageHandler).GetMethod(StorageMethod.ENTITY.Value
                    , BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(Dictionary<Type, List<IMeasurement>>) }, null);
                methodInfo.Invoke(this, new object[] { measurements });
            }
            return null;
        }
        #endregion

        #region Configuration Methods
        /// <summary>
        /// Reads the active storage method from the app configuration.
        /// </summary>
        /// <returns>The current active storage method.</returns>
        private StorageMethod GetActiveStorageMethod()
        {
            return StorageMethod.Convert(ConfigurationManager.AppSettings[Strings.Config.DataStorageMode.Value]);
        }

        /// <summary>
        /// Transforms a list of strings to a list of StorageMethod objects.
        /// </summary>
        /// <returns>A list of the configured storage methods.</returns>
        private List<StorageMethod> GetConfiguredStorageMethods()
        {
            List<StorageMethod> configuredStorageMethods = new List<StorageMethod>();
            List<string> strStorageMethods = ExtractConfiguredStoragemethods();
            foreach (string strStorageMethod in strStorageMethods)
            {
                configuredStorageMethods.Add(StorageMethod.Convert(strStorageMethod));
            }
            return configuredStorageMethods;
        }
        #endregion

        #region Data Parsing Methods
        /// <summary>
        /// Extracts the configured storage methods from the App.config.
        /// </summary>
        /// <returns>A List that contains the configured storage methods</returns>
        private List<string> ExtractConfiguredStoragemethods()
        {
            List<string> dataStorageMethods = new List<string>();

            // Extract all confguration keys from app.config.
            string currentProccessName = Process.GetCurrentProcess().ProcessName;
            string appConfigFilePath = Directory.GetCurrentDirectory() + "\\" + currentProccessName + FileExtensions.ExeConfig;
            XDocument doc = XDocument.Load(appConfigFilePath);
            var result = doc.Descendants(Strings.String.AppSettings.Value).Descendants(Strings.String.Add.Value).Attributes(Strings.String.Key.Value);

            // Add all storage methods to the returning list.
            foreach (var item in result)
            {
                if (item.Value.Equals(Strings.Config.DataStorageMethod))
                {
                    dataStorageMethods.Add(item.Value);
                }
            }
            return dataStorageMethods;
        }
        #endregion

        #region Validation Methods
        private bool ValidateStorageMethod(StorageMethod activeMethod, List<StorageMethod> configuredMethods)
        {
            foreach (StorageMethod storageMethod in configuredMethods)
            {
                if (activeMethod.Value == storageMethod.Value) return true;
            }
            return false;
        }
        #endregion

        #region Data Storing Methods
        /// <summary>
        /// Invoked dynamically by StoreData method. Its main responsbility is to start the entity database 
        /// handler in order to store the passed data.
        /// </summary>
        /// <param name="measurements"></param>
        private void ENTITY(Dictionary<Type, List<IMeasurement>> measurements)
        {
            MeasurementsDbDataService dbDataHandler = new MeasurementsDbDataService();
            dbDataHandler.Insert(measurements);
        }

        /// <summary>
        /// Invoked dynamically by StoreData method. Its main responsibiltiy is to start the RestClient in order to send the data to 
        /// the configured API.
        /// </summary>
        /// <param name="measurements"></param>
        private void API(Dictionary<Type, List<IMeasurement>> measurements)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invoked dynamically by StoreData method. Its main responsibility is to start the FileClient in order to save the data to the 
        /// configured file, with the specified format.
        /// </summary>
        /// <param name="measurements"></param>
        private void FILE(Dictionary<Type, List<IMeasurement>> measurements)
        {
            FileDataService fileService = new FileDataService();
            fileService.Store(measurements);
        }
        #endregion
    }
}
