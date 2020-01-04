using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecibosAxosPersistence.Context;
using RecibosAxosPersistence.Models;

namespace RecibosAxosTest
{
    [TestClass]
    public class PruebasGuardado
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void CrearProvedor()
        {
            using (var context= new ApplicationDbContext())
            {
                context.Set<Provedor>().Add(new Provedor { Nombre= "Test1"});
                if (context.SaveChanges() <= 0)
                    throw new Exception("Ocurrio un error");
            }
        }
    }
}
