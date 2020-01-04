using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecibosAxosApi.Utils
{
    public class ListUtil
    {
        public string name { get; set; }
        public string value { get; set; }
        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public ListUtil(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        /// <summary>
        /// Constructor sin parametros establece vacios los atributos
        /// </summary>
        public ListUtil()
        {

        }
    }
}