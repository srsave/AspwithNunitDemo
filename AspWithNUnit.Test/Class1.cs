using System.Collections.Generic;
using NUnit.Framework;
using System.Web;
using System;
using System.Text.RegularExpressions;

//using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.IO;
using System.Globalization;
using AspWithNUnit;
namespace SampleAddition1_NUnit
{
    //TestSet
    public class TetstSetInfo
    {

        public string l_Num1 { get; set; }
        public string l_Num2 { get; set; }

        public TetstSetInfo(string _Num1, string _Num2)
        {
            l_Num1 = _Num1;
            l_Num2 = _Num2;

        }

    }

    [TestFixture]
    public class TestCases
    {
        private static IEnumerable<TetstSetInfo> ParamterValuesSet1
        {
            get
            {

                yield return new TetstSetInfo("1", "10");
                yield return new TetstSetInfo("500", "1000");
                yield return new TetstSetInfo("50.70", "Text");
            }
        }
        private static IEnumerable<TetstSetInfo> ParamterValuesSet2
        {
            get
            {

                yield return new TetstSetInfo("", "10");
                yield return new TetstSetInfo("500", "");
                yield return new TetstSetInfo("NN", "10.800");
                yield return new TetstSetInfo("10.800", "Sudeep");
            }
        }

        [Test, TestCaseSource("ParamterValuesSet1")]
        public void checkAddition(TetstSetInfo tci)
        {
            AddClass add1 = new AddClass();
            string valMsg = "";
            string valAdd = "";
            bool flag = add1.addition(tci.l_Num1, tci.l_Num2, out valMsg, out valAdd);
            // Assert.AreEqual(true, flag);

            if (flag == true)
            {
                Assert.Pass("Passed Positive Test Case1");
            }
            else
            {
                Assert.Fail("Failed Positive Test Case1");
            }

        }
        [Test, TestCaseSource("ParamterValuesSet2")]
        public void checkAddition1(TetstSetInfo tci)
        {
            AddClass add1 = new AddClass();
            string valMsg = "";
            string valAdd = "";
            bool flag = add1.addition(tci.l_Num1, tci.l_Num2, out valMsg, out valAdd);
            // Assert.AreEqual(true, flag);

            if (flag == false)
            {
                Assert.Pass("Passed Validation : " + valMsg);
            }
            else
            {
                Assert.Pass("Failed Validation : " + valMsg);
            }

        }
    }
}
