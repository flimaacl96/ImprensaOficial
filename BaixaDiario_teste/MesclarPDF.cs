using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp;
using System.IO;
//using BaixaDiario_teste;

namespace MesclarPDF
{
    class MesclaPDF
    {
        internal static void Principal()//string[] args)
        {
            string[] files = GetFiles();

            PdfDocument outputDocument = new PdfDocument();

            foreach (string file in files)
            {
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = inputDocument.Pages[idx];
                    outputDocument.AddPage(page);
                }
                
            }

            string dia = Convert.ToString(DateTime.Now.Day);
            string mes = Convert.ToString(DateTime.Now.Month);
            string ano = Convert.ToString(DateTime.Now.Year);
            string data = dia + "-" + "0" + mes + "-" + ano;

            //salvar
            string nomeArquivo = @"c:\DiarioBaixado\Diario"+data+".pdf";
            outputDocument.Save(nomeArquivo);

        }

        private static string[] GetFiles()
        {
            DirectoryInfo di = new DirectoryInfo(@"c:\DiarioBaixado\temp");
            FileInfo[] files = di.GetFiles("*.pdf");
            int i = 0;
            string[] names = new string[files.Length];
            foreach(var r in files)
            {
               names[i] = r.FullName;
                i = i + 1;
            }

            return names;
        }

        
    }
}
