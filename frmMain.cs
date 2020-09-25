using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zebra.Sdk.Comm;

namespace Megah_Motor_Label
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
    }

    private void btn2Reset_Click(object sender, EventArgs e)
    {
      txt2AsalBarang.Text = "";
      txt2NamaBarang.Text = "";
      txt2TipeMobil.Text = "";
      txt2KodeJual.Text = "";
      txt2KodeMandarin.Text = "";
      txt2HurufMandarin.Text = "";
      num2Cetak.Value = 0;
      txt2NamaBarang.Focus();
    }

    private void btn2Cetak_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Apakah anda yakin untuk mencetak?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        if (txt2AsalBarang.Text == "" || txt2NamaBarang.Text == "" || txt2TipeMobil.Text == "" || txt2KodeJual.Text == "" || txt2HurufMandarin.Text == "")
        {
          MessageBox.Show("Kolom tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
          for(int i = 0; i <= num2Cetak.Value; i++)
          {
            cetak_2();
          }
        }
      };
    }

    private void cetak_2()
    {
      try
      {
        string text = System.IO.File.ReadAllText("./settings2.txt");
        var thePrinterConn = ConnectionBuilder.Build(text);
        try
        {
          // Open the connection - physical connection is established here.
          thePrinterConn.Open();

          // This example prints "This is a ZPL test." near the top of the label.
          string zplData = "^XA" +
            //item id
            "^FO015,21^A0,34,24^FD" + txt2AsalBarang.Text + "^FS" +
            "^FO440,21^A0,34,24^FD" + txt2AsalBarang.Text + "^FS" +
            //item name 1
            "^FO015,55^A0,34,24^FD" + txt2NamaBarang.Text + "^FS" +
            "^FO440,55^A0,34,24^FD" + txt2NamaBarang.Text + "^FS" +
            //item name 2
            "^FO015,89^A0,34,24^FD" + txt2TipeMobil.Text + "^FS" +
            "^FO440,89^A0,34,24^FD" + txt2TipeMobil.Text + "^FS" +
            //item name 3
            "^FO015,123^A0,34,24^FD" + txt2KodeJual.Text + "^FS" +
            "^FO440,123^A0,34,24^FD" + txt2KodeJual.Text + "^FS" +
            //item chinese
            "^FO015,156^CI28^A@N,36,30,E:SIMSUN.FNT^FD" + txt2HurufMandarin.Text + "^FS" +
            "^FO440,156^CI28^A@N,36,30,E:SIMSUN.FNT^FD" + txt2HurufMandarin.Text + "^FS" +
            "^XZ";

          // Send the data to printer as a byte array.
          thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
        }
        catch (ConnectionException ex)
        {
          // Handle communications error here.
          Console.WriteLine(ex.ToString());
        }
        finally
        {
          // Close the connection to release resources.
          thePrinterConn.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString(), "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      txt2NamaBarang.Text = "";
      txt2TipeMobil.Text = "";
      txt2KodeJual.Text = "";
      txt2KodeMandarin.Text = "";
      txt2HurufMandarin.Text = "";
      num2Cetak.Value = 0;
      txt2NamaBarang.Focus();
    }

    private void txt2NamaBarang_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txt2AsalBarang.Text = "";
        txt2AsalBarang.Focus();
      }
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt2TipeMobil.Focus();
      }
    }
    private void print_label2()
    {
      String asalBarang = txt2AsalBarang.Text;
      String namaBarang = txt2NamaBarang.Text;
      String tipeMobil = txt2TipeMobil.Text;
      String kodeJual = txt2KodeJual.Text;
      String kodeMandarin = txt2KodeMandarin.Text;
      //Connection thePrinterConn = new TcpConnection("127.0.0.1", TcpConnection.DEFAULT_ZPL_TCP_PORT);
      ///Connection thePrinterConn = new UsbConnection("Port_#0001.Hub_#0004");
      var thePrinterConn = ConnectionBuilder.Build("USB:ZDesigner GT800 (ZPL)");
      try
      {
        // Open the connection - physical connection is established here.
        thePrinterConn.Open();

        // This example prints "This is a ZPL test." near the top of the label.
        string zplData = "^XA" +
          //item id
          "^FO18,22^A0,38,26^FD" + asalBarang + "^FS" +
          "^FO435,22^A0,38,26^FD" + asalBarang + "^FS" +
          //item name 1
          "^FO18,55^A0,38,26^FD" + namaBarang + "^FS" +
          "^FO435,55^A0,38,26^FD" + namaBarang + "^FS" +
          //item name 2
          "^FO18,88^A0,38,26^FD" + tipeMobil + "^FS" +
          "^FO435,88^A0,38,26^FD" + tipeMobil + "^FS" +
          //item name 3
          "^FO18,121^A0,38,26^FD" + kodeJual + "^FS" +
          "^FO435,121^A0,38,26^FD" + kodeJual + "^FS" +
          //item chinese
          "^FO18,154^CI28^A@N,40,40,E:SIMSUN.FNT^FD" + kodeMandarin + "^FS" +
          "^FO435,154^CI28^A@N,40,40,E:SIMSUN.FNT^FD" + kodeMandarin + "^FS" +
          "^XZ";

        // Send the data to printer as a byte array.
        thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
      }
      catch (ConnectionException e)
      {
        // Handle communications error here.
        Console.WriteLine(e.ToString());
      }
      finally
      {
        // Close the connection to release resources.
        thePrinterConn.Close();
      }
    }

    private void txt2AsalBarang_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt2NamaBarang.Focus();
      }
    }

    private void txt2TipeMobil_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt2KodeJual.Focus();
      }
    }

    private void txt2KodeJual_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt2KodeMandarin.Focus();
      }
    }

    private void num2Cetak_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        btn2Cetak.Focus();
      }
    }

    private String angkaCina(int angka)
    {
      String[] digits = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
      String[] positions = { "", "十", "百", "千", "万", "十万", "百万", "千万", "亿", "十亿", "百亿", "千亿" };
      Char[] charArray = angka.ToString().ToCharArray();
      String result = "";
      bool prevIsZero = false;

      for (int i = 0; i < charArray.Length; i++)
      {
        Char ch = charArray[i];
        if (ch != '0' && !prevIsZero)
        {
          result += digits[(int)Char.GetNumericValue(ch)] + positions[charArray.Length - i - 1];
        }
        else if (ch == '0')
        {
          prevIsZero = true;
        }
        else if (ch != '0' && prevIsZero)
        {
          result += '零' + digits[(int)Char.GetNumericValue(ch)] + positions[charArray.Length - i - 1];
        };

      }
      return result;
    }

    private void btn2Mandarin_Click(object sender, EventArgs e)
    {
      String kodeMandarin = txt2KodeMandarin.Text;
      int value;
      if (int.TryParse(kodeMandarin, out value))
      {
        txt2HurufMandarin.Text = angkaCina(int.Parse(kodeMandarin));
      }
      else
      {
        MessageBox.Show("Konversi hanya berupa angka!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void txt2KodeMandarin_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt2HurufMandarin.Focus();
      }
    }

    private void txt2HurufMandarin_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        num2Cetak.Focus();
      }
    }

    private void txt3AsalBarang_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt3NamaBarang.Focus();
      }
    }

    private void txt3NamaBarang_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txt3AsalBarang.Text = "";
        txt3AsalBarang.Focus();
      }
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt3TipeMobil.Focus();
      }
    }

    private void txt3TipeMobil_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt3KodeJual.Focus();
      }
    }

    private void txt3KodeJual_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt3KodeMandarin.Focus();
      }
    }

    private void txt3KodeMandarin_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        txt3HurufMandarin.Focus();
      }
    }

    private void txt3HurufMandarin_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        num3Cetak.Focus();
      }
    }

    private void num3Cetak_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
      {
        btn3Cetak.Focus();
      }
    }

    private void btn3Reset_Click(object sender, EventArgs e)
    {
      txt3AsalBarang.Text = "";
      txt3NamaBarang.Text = "";
      txt3TipeMobil.Text = "";
      txt3KodeJual.Text = "";
      txt3KodeMandarin.Text = "";
      txt3HurufMandarin.Text = "";
      num3Cetak.Value = 0;
      txt3NamaBarang.Focus();
    }

    private void btn3Cetak_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Apakah anda yakin untuk mencetak?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        if (txt3AsalBarang.Text == "" || txt3NamaBarang.Text == "" || txt3TipeMobil.Text == "" || txt3KodeJual.Text == "" || txt3HurufMandarin.Text == "")
        {
          MessageBox.Show("Kolom tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
          for (int i = 0; i <= num3Cetak.Value; i++)
          {
            cetak_3();
          }
        }
      };
    }

    private void cetak_3()
    {
      try
      {
        string text = System.IO.File.ReadAllText("./settings3.txt");
        var thePrinterConn = ConnectionBuilder.Build(text);
        try
        {
          // Open the connection - physical connection is established here.
          thePrinterConn.Open();

          // This example prints "This is a ZPL test." near the top of the label.
          string zplData = "^XA" +
            //item id
            "^FO015,21^A0,34,24^FD" + txt3AsalBarang.Text + "^FS" +
            "^FO300,21^A0,34,24^FD" + txt3AsalBarang.Text + "^FS" +
            "^FO590,21^A0,34,24^FD" + txt3AsalBarang.Text + "^FS" +
            //item name 1
            "^FO015,55^A0,34,24^FD" + txt3NamaBarang.Text + "^FS" +
            "^FO300,55^A0,34,24^FD" + txt3NamaBarang.Text + "^FS" +
            "^FO590,55^A0,34,24^FD" + txt3NamaBarang.Text + "^FS" +
            //item name 2
            "^FO015,89^A0,34,24^FD" + txt3TipeMobil.Text + "^FS" +
            "^FO300,89^A0,34,24^FD" + txt3TipeMobil.Text + "^FS" +
            "^FO590,89^A0,34,24^FD" + txt3TipeMobil.Text + "^FS" +
            //item name 3
            "^FO015,123^A0,34,24^FD" + txt3KodeJual.Text + "^FS" +
            "^FO300,123^A0,34,24^FD" + txt3KodeJual.Text + "^FS" +
            "^FO590,123^A0,34,24^FD" + txt3KodeJual.Text + "^FS" +
            //item chinese
            "^FO015,156^CI28^A@N,36,30,E:SIMSUN.FNT^FD" + txt3HurufMandarin.Text + "^FS" +
            "^FO300,156^CI28^A@N,36,30,E:SIMSUN.FNT^FD" + txt3HurufMandarin.Text + "^FS" +
            "^FO590,156^CI28^A@N,36,30,E:SIMSUN.FNT^FD" + txt3HurufMandarin.Text + "^FS" +
            "^XZ";

          // Send the data to printer as a byte array.
          thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
        }
        catch (ConnectionException ex)
        {
          // Handle communications error here.
          Console.WriteLine(ex.ToString());
        }
        finally
        {
          // Close the connection to release resources.
          thePrinterConn.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString(), "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      txt3NamaBarang.Text = "";
      txt3TipeMobil.Text = "";
      txt3KodeJual.Text = "";
      txt3KodeMandarin.Text = "";
      txt3HurufMandarin.Text = "";
      num3Cetak.Value = 0;
      txt3NamaBarang.Focus();
    }

    private void btn3Mandarin_Click(object sender, EventArgs e)
    {
      String kodeMandarin = txt3KodeMandarin.Text;
      int value;
      if (int.TryParse(kodeMandarin, out value))
      {
        txt3HurufMandarin.Text = angkaCina(int.Parse(kodeMandarin));
      }
      else
      {
        MessageBox.Show("Konversi hanya berupa angka!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

  }
}
