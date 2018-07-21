using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ServiceReference1
{
    public partial class Usuario
    {
        public Uri ImageUri
        {
            get
            {
                if(String.IsNullOrEmpty(Imagen))
                    return new System.Uri("http://www.tr.all.biz/img/tr/catalog/middle/187959.jpeg");
                else
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/users/" + this._id.ToString()+".jpg");
            }
        }
    }
}
