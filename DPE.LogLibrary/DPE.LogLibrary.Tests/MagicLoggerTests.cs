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
        private const string ErrorFilePath = LogPath + @"\error.txt";
        private const string InformationFilePath = LogPath + @"\information.txt";
        private MagicLogger _logger ;

        [SetUp]
        public void SetUp()
        {
            Helper.AddKeyValueInAppConfig(LogPathKey, LogPath);
            _logger =new MagicLogger();
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
            var dic = new Dictionary<string, object> { { "controller", "AppleController" } };
            _logger.WriteError("msg", dic);

            var errorContents = Helper.ReadFile(ErrorFilePath);
            var infoContents = Helper.ReadFile(InformationFilePath);

            errorContents.Should().NotBeNull();
            infoContents.Should().BeNull();
        }

        [Test]
        public void should_save_error_log_with_txtformatter()
        {
            const string message = "msg";
            var dict = new Dictionary<string, object>
            {
                { "Controller", "AppleController" },
                { "RequestUri", "http://localhost:1234/Controller/" },
            };
            _logger.WriteError(message, dict);

            var errorContents = Helper.ReadFile(ErrorFilePath);

            const string expectedResult = "----------------------------------\r\n"+
                                           "Controller : AppleController\r\n"+
                                           "RequestUri : http://localhost:1234/Controller/\r\n" +
                                           "Message : msg\r\n"+
                                           "----------------------------------\r\n";

            errorContents.Should().NotBeNull();
            errorContents.Should().Be(expectedResult);
        }

        [Test]
        public void should_log_value_with_null_when_incoming_value_is_null_or_empty()
        {
            const string message = "msg";
            var dict = new Dictionary<string, object>
            {
                { "Controller", "" },
                { "RequestUri", null },
            };
            _logger.WriteError(message, dict);

            var errorContents = Helper.ReadFile(ErrorFilePath);

            const string expectedResult = "----------------------------------\r\n" +
                                           "Controller : null\r\n" +
                                           "RequestUri : null\r\n" +
                                           "Message : msg\r\n" +
                                           "----------------------------------\r\n";

            errorContents.Should().NotBeNull();
            errorContents.Should().Be(expectedResult);
        }

        [TearDown]
        public void TearDown()
        {
            Helper.RemoveFile(ErrorFilePath);
            Helper.RemoveFile(InformationFilePath);
        }
    }
}
