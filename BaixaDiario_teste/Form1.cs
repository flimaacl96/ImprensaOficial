using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//bibliotecas usadas no iTextSharp
using System.IO;
using iTextSharp;
using iTextSharp.text;
using MesclarPDF;


namespace BaixaDiario_teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        


        private void button1_Click(object sender, EventArgs e)
        {
                        
            //validação da checkbox (SP)
            if (checkBox1.Checked)
            {
                //validação das textbox (pag inicial e pag final
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Insira a página inicial", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Insira a página final", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    //conteudo das texbox
                    decimal inicio = Convert.ToDecimal(textBox1.Text);
                    decimal fim = Convert.ToDecimal(textBox2.Text);
                    decimal numPag = 0;

                    //variaveis de data e validação de mês
                    var dia = DateTime.Now.Day;
                    var ano = DateTime.Now.Year;
                    var mes = DateTime.Now.Month;
                    var nomeMes = "";

                    //validacao do nome do mes
                    if (mes == 1)
                    {
                        nomeMes = "janeiro";
                    }
                    if (mes == 2)
                    {
                        nomeMes = "fevereiro";
                    }
                    if (mes == 3)
                    {
                        nomeMes = "marco";
                    }
                    if (mes == 4)
                    {
                        nomeMes = "abril";
                    }
                    if (mes == 5)
                    {
                        nomeMes = "maio";
                    }
                    if (mes == 6)
                    {
                        nomeMes = "junho";
                    }
                    if (mes == 7)
                    {
                        nomeMes = "julho";
                    }
                    if (mes == 8)
                    {
                        nomeMes = "agosto";
                    }
                    if (mes == 9)
                    {
                        nomeMes = "setembro";
                    }
                    if (mes == 10)
                    {
                        nomeMes = "outubro";
                    }
                    if (mes == 11)
                    {
                        nomeMes = "novembro";
                    }
                    if (mes == 12)
                    {
                        nomeMes = "dezembro";
                    }

                                //criando diretorios para guardar arquivos PDF
                                System.IO.Directory.CreateDirectory(@"C:\DiarioBaixado");
                                System.IO.Directory.CreateDirectory(@"C:\DiarioBaixado\Temp");


                                //validacao de dia da semana, visto que o diario nao esta disponivel na segunda feira
                                //sendo assim, se o programa for executado no domingo ou segunda feira, ele corrige o dia para o sábado
                                string diaSemana = Convert.ToString(DateTime.Now.DayOfWeek);
                                if (diaSemana == "Sunday")
                                {
                                    dia = dia - 1;
                                }
                                if (diaSemana == "Monday")
                                {
                                    dia = dia - 2;
                                }
                                else { 


                    //download das paginas
                    for (numPag = inicio; numPag <= fim; numPag++)
                    {
                        if (numPag < 10)
                        {
                            WebClient webClient = new WebClient();
                            webClient.DownloadFile("http://diariooficial.imprensaoficial.com.br/doflash/prototipo/" + ano + "/" + nomeMes + "/" + dia + "/exec1/pdf/pg_000" + numPag + ".pdf", @"c:\DiarioBaixado\Temp\Pag" + numPag + ".pdf");

                                                       
                        }
                        else if (numPag >= 10 && numPag < 100)
                        {
                            WebClient webClient = new WebClient();
                            webClient.DownloadFile("http://diariooficial.imprensaoficial.com.br/doflash/prototipo/" + ano + "/" + nomeMes + "/" + dia + "/exec1/pdf/pg_00" + numPag + ".pdf", @"c:\DiarioBaixado\Temp\Pag" + numPag + ".pdf");

                            
                        }
                        else if (numPag > 100 && numPag <= fim)
                        {
                            WebClient webClient = new WebClient();
                            webClient.DownloadFile("http://diariooficial.imprensaoficial.com.br/doflash/prototipo/" + ano + "/" + nomeMes + "/" + dia + "/exec1/pdf/pg_0" + numPag + ".pdf", @"c:\DiarioBaixado\Temp\Pag" + numPag + ".pdf");

                                                        
                        }
                    }
                                }           
                }
            }
            else
            {
                //este ELSE serve para fechar validação de textbox; precisa incluir outro IF para diario de MG;
                MessageBox.Show("Selecione um Diário", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //chamada da classe que mescla os arquivos PDF
            MesclaPDF.Principal();

            //apaga a pasta temporaria que foi criada para os arquivos individuais
            Directory.Delete(@"C:\DiarioBaixado\Temp", true);

            MessageBox.Show("Execução Concluída"+"\n"+ "Seu arquivo está em C:\\DiarioBaixado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBox1.Clear();
            textBox2.Clear();

                                
        }

    }
}
