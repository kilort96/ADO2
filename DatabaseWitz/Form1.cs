using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DatabaseWitz
{
    public partial class Form1 : Form
    {
        OleDbConnection con=null;
        OleDbCommand cmd = null;
        OleDbDataReader reader = null;
        List<Artikel> artikelList = null;
        public Form1()
        {
            InitializeComponent();
            artikelList = new List<Artikel>();
        }
        private void updateArtikel(Artikel a)
        {
            //TODO: Command Objekt
            OleDbCommand cmd = con.CreateCommand();
            //TPDO: Parameter generien
            cmd.Parameters.AddWithValue("ANR", a.ArtikelNr);
            cmd.Parameters.AddWithValue("BEZ", a.Bezeichnung);
            cmd.Parameters.AddWithValue("BESTAND", a.Bestand);
            cmd.Parameters.AddWithValue("MBEST",a.Meldebestand);
            cmd.Parameters.AddWithValue("VKPREIS", a.VkPreis);
            cmd.Parameters.AddWithValue("ENT", a.LetzteEntnahme);

            //TODO: Commandtext: SQL
            String sql = "UPDATE tArtikel SET ArtikelNr = ANR,Bezeichnung = BEZ, Bestand = Bestand, Meldebestand = MBEST, VkPreis = VKPREIS, LetzteEntnahme = ENT";
            sql += "WHERE ArtikelOID =" + a.ArtikelOid.ToString();
            cmd.CommandText = sql;
            //TODO: Conn open
            con.Open();
            //TODO: Command ausführen
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Fehler beim Update");
                toolStripStatusLabel1.Text = exc.Message;
            }
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source="Bestellung.accdb"
            con = new OleDbConnection();
            //OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            //builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            //builder.DataSource = "Bestellung.accdb";
            con.ConnectionString = Properties.Settings.Default.DbCon;

            try
            {
                con.Open();
                toolStripStatusLabel1.Text = "Verbindung aufgebaut";
            }
            catch (InvalidOperationException ex)
            {
                toolStripStatusLabel1.Text = "Verbindung ist bereits geöffnet";

            }
            catch (OleDbException ole)
            {
                MessageBox.Show(ole.Message);
            }

        }

        private void buttonCommand_Click(object sender, EventArgs e)
        {
            
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tArtikel";
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                MessageBox.Show("Zugriff auf tArtikel nicht möglich");
            }
            
        }

        private void Anzeigen_Click(object sender, EventArgs e)
        {
            while(reader.Read()==true)
            {

                //String bez= reader.GetString(3);
                Artikel a = mkArtikelObject(reader);
                //listBoxAusgabe.Items.Add(a);
                artikelList.Add(a);
            }
            listBoxAusgabe.DataSource = artikelList;
            reader.Close();
        }

        private Artikel mkArtikelObject(OleDbDataReader reader)
        {
            Artikel a = new Artikel();
            a.ArtikelOid = reader.GetInt32(0);
            try
            {
                a.ArtikelNr = reader.GetString(1);
            }
            catch (Exception)
            {

                
            }
            a.ArtikelGruppe = reader.GetInt32(2);
            a.Bezeichnung = reader.GetString(3);
            try
            { 
                 a.Bestand = reader.GetByte(4);
            }
            catch(Exception)
            {

            }


            a.Meldebestand = reader.GetInt16(5);
            a.Verpackung = reader.GetInt32(6);
            a.VkPreis = reader.GetDecimal(7);
            try
            {
                a.LetzteEntnahme = reader.GetDateTime(8);
            }
            catch(Exception)
            {

            }
            return a;
        }

        private void buttonDatensatz_Click(object sender, EventArgs e)
        {
            if (listBoxAusgabe.SelectedItem != null)
            {
                Artikel a = (Artikel)listBoxAusgabe.SelectedItem;
                FormUpdate frmUpdate = new FormUpdate();
                frmUpdate.ShowDialog(); //modales Fenster
                if(frmUpdate.Result == DialogResult.OK)
                {
              
                    updateArtikel(a);
                }
                else
                {
                    toolStripStatusLabel1.Text = "Änderung wurde Abgebrochen";
                }
            }
            
        }
    }
}
