using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseWitz
{
    public partial class FormUpdate : Form
    {
        private Artikel selArtikel;
        private DialogResult result = DialogResult.OK;

        public DialogResult Result
        {
            get
            {
                return Result1;
            }

            set
            {
                Result1 = value;
            }
        }

        public DialogResult Result1
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public Artikel SelArtikel
        {
            get
            {
                return selArtikel;
            }

            set
            {
                selArtikel = value;
            }
        }

        public FormUpdate()
        {
            InitializeComponent();
        }

       public FormUpdate(Artikel artikel):this()
        {
            SelArtikel = artikel;
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.textBoxid.Text = SelArtikel.ArtikelOid.ToString();
            this.textBoxNummer.Text = SelArtikel.ArtikelNr.ToString();
            this.textBoxArtikelGruppe.Text = SelArtikel.ArtikelNr.ToString();
            this.textBoxBezeichnung.Text = SelArtikel.Bezeichnung;
            this.textBoxBestand.Text = SelArtikel.Bezeichnung.ToString();
            this.textBoxMeldebestand.Text = SelArtikel.Meldebestand.ToString();
            this.textBoxVerpackung.Text = SelArtikel.Verpackung.ToString();
            this.textBoxVerkaufspreis.Text = SelArtikel.VkPreis.ToString();
        }

        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SelArtikel.ArtikelNr = textBoxNummer.Text;
            SelArtikel.Bezeichnung = textBoxBezeichnung.Text;
            SelArtikel.Bestand = Convert.ToByte(textBoxBestand.Text);
            SelArtikel.Meldebestand = Convert.ToInt16(textBoxMeldebestand.Text);
            SelArtikel.VkPreis = Convert.ToDecimal(textBoxVerkaufspreis.Text);
            SelArtikel.LetzteEntnahme = Convert.ToDateTime(textBoxEntnahme.Text);
            this.Close();
        }
    }
}
