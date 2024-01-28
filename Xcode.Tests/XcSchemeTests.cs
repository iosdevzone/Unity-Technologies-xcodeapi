using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
#if UNITY_XCODE_API_BUILD
using UnityEditor.iOS.Xcode;
#else
using UnityEditor.iOS.Xcode.Custom;
#endif

namespace UnityEditor.iOS.Xcode.Tests
{
    
    [TestFixture]
    public class XcSchemeUpdating : TextTester
    {
        public XcSchemeUpdating() : base("XcSchemeTestFiles", "XcSchemeTestOutput", debug:false)
        {
        }

        [Test]
        public void ChangingBuildConfigurationWorks()
        {
            var xcscheme = new XcScheme();
            xcscheme.ReadFromString(ReadSourceFile("base1.xcscheme"));
            Assert.That(xcscheme.GetBuildConfiguration(), Is.EqualTo("Debug"));
            xcscheme.SetBuildConfiguration("MyConfiguration");
            Assert.That(xcscheme.GetBuildConfiguration(), Is.EqualTo("MyConfiguration"));
        }

        [Test]
        public void OutputWorks()
        {
            TestXmlUpdate("base1.xcscheme", "test1.xcscheme", text => 
            {
                var xcscheme = new XcScheme();
                xcscheme.ReadFromString(text);
                xcscheme.SetBuildConfiguration("ReleaseForRunning");
                return xcscheme.WriteToString();
            });
        }
    }
}
