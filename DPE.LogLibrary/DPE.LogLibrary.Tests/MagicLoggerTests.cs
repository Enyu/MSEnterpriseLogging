using System;
using System.Collections.Generic;
using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using Helper = DPE.LogLibrary.Tests.TestHelper.TestHelper;

namespace DPE.LogLibrary.Tests
{
    [TestFixture]
    public class MagicLoggerTests
    {
        const string LogPathKey = "logPath";
        const string LogPath = @"C:\log";
        private const string errorFile = LogPath + @"\error.txt";
        private const string informationFile = LogPath + @"\information.txt";

        [SetUp]
        public void SetUp()
        {
            Helper.AddKeyValueInAppConfig(LogPathKey, LogPath);
        }

        [Test]
        public void should_throw_configuration_error_exception_when_user_does_not_set_log_path()
        {
            Helper.RemoveKeyFromAppConfig(LogPathKey);
            Exception caughtException = null;
            
            try
            {
                var logger = new MagicLogger();
                var dic = new Dictionary<string, object> { { "controller", "AppleController" } };
                logger.WriteError("msg", dic);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }
            
            caughtException.Should().NotBeNull();
            caughtException.GetType().Should().Be(typeof(ConfigurationErrorsException));
            caughtException.Message.Should().Be("Please add log path root in your config file first.");
        }

        [Test]
        public void should_only_log_error_successfully()
        {
            var logger = new MagicLogger();
            var dic = new Dictionary<string, object> { { "controller", "AppleController" } };
            logger.WriteError("msg", dic);

            var errorContents = Helper.ReadFile(errorFile);
            var infoContents = Helper.ReadFile(informationFile);
            errorContents.Should().NotBeNull();
            infoContents.Should().BeNull();
        }

        [TearDown]
        public void TearDown()
        {
            Helper.RemoveFile(errorFile);
            Helper.RemoveFile(informationFile);
        }
    }
}
