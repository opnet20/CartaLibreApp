using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ServiceReference1
{
    public partial class Comentario
    {
        public Uri ImageUser
        {
            get
            {
                if (String.IsNullOrEmpty(this.ImagenUsuario))
                    return new System.Uri("http://www.tr.all.biz/img/tr/catalog/middle/187959.jpeg");
                else
                {
                    if (this.ImagenUsuario == "si")
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/users/" + this.idUsuario.ToString() + ".jpg");
                    else
                        return new System.Uri(this.ImagenUsuario);
                }
            }
        }
    }
}
