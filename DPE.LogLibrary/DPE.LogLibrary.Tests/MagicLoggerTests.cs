using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;
using Helper = DPE.LogLibrary.Tests.TestHelper.TestHelper;

namespace DPE.LogLibrary.Tests
{
    [TestFixture]
    public class MagicLoggerTests
    {
        [SetUp]
        public void SetUp()
        {
            Helper.AddKeyValueInAppConfig("logPath", @"C:\log");
        }

        [Test]
        public void should_throw_configuration_error_exception_when_user_does_not_set_log_path()
        {
            Helper.RemoveKeyFromAppConfig("logPath");
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

    }
}
