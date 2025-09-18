using System;
using System.Collections.Generic;
using entMerchPlus;
using Microsoft.Extensions.Logging;
using SQLite;

namespace MerchPlusMAUI.Services
{
    public class DemoDataGenerator
    {
        private readonly ILogger<DemoDataGenerator> _logger;
        private readonly string _dbPath;
        
        public DemoDataGenerator(ILogger<DemoDataGenerator> logger)
        {
            _logger = logger;
            _dbPath = GlobalVariables.DatabasePath;
        }
        
        /// <summary>
        /// Demo data generation is disabled - using only real service data
        /// </summary>
        public bool GenerateDemoData()
        {
            _logger.LogInformation("Demo data generation is disabled - application will use only real service data");
            return true;
        }
    }
} 