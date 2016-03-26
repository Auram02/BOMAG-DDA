using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace DDA.Tests
{
    [TestFixture]
    public class ContractTests
    {
        [SetUp]
        public void Init()
        {

        }

        [Test, Category("Tests")]
        public void GetContractTest()
        {
            DDA.BusinessLogic.InitializeProgram.InitializeGlobals();
            DDA.DataAccess.Contract_da.GetContract(55);


        }
    }
}
