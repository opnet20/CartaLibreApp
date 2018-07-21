using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.Model
{
    public partial class ItemLista
    {
        string icono;
        string titulo;
        string categoria;

        public string Icono
        {
            get
            {
                return icono;
            }

            set
            {
                icono = value;
            }
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
            }
        }

        public string Categoria
        {
            get
            {
                return categoria;
            }

            set
            {
                categoria = value;
            }
        }
    }
}
