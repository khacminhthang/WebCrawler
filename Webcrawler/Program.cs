using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fizzler.Systems.HtmlAgilityPack;

namespace Webcrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            // load trang web, nap html vao document
            HtmlDocument document = htmlWeb.Load("https://www.webtretho.com/forum/f26/");
            var items = new List<object>();
            //
            //SỬ DỤNG XPATH ĐỂ TÌM PHẦN TỬ
            //
            // load cac tag li trong tag ul
            //var threadItems = document.DocumentNode.SelectNodes("//ul[@id='threads']/li").ToList();

            //foreach (var item in threadItems)
            //{
            //    //Extract cac gia tri tu cac tag con cua tag li
            //    var linkNode = item.SelectSingleNode(".//a[contains(@class,'title')]");
            //    var link = linkNode.Attributes["href"].Value;
            //    var text = linkNode.InnerText;
            //    var readCount = item.SelectSingleNode(".//div[@class='folTypPost']/ul/li/b").InnerText;

            //    items.Add(new { text, readCount, link });
            //}

            //
            //SỬ DỤNG LINQ TO OBJECT ĐỂ TÌM NODE
            //
            //var threadItems = document.DocumentNode.Descendants("ul")
            //    .First(node => node.Attributes.Contains("id") && node.Attributes["id"].Value == "threads")
            //    .ChildNodes.Where(node => node.Name == "li").ToList();
            //foreach (var item in threadItems)
            //{
            //    var linkNode = item.Descendants("a").First(node => node.Attributes.Contains("class") && node.Attributes["class"].Value.Contains("title"));
            //    var link = linkNode.Attributes["href"].Value;
            //    var text = linkNode.InnerText;
            //    var readCount = item.Descendants("b").First().InnerText;

            //    items.Add(new { text, readCount, link });
            //}


            //
            //CẢI TIẾN VỚI FIZZLER
            //
            var threadItems = document.DocumentNode.QuerySelectorAll("ul#threads > li").ToList();

            foreach (var item in threadItems)
            {
                var linkNode = item.QuerySelector("a.title");
                var link = linkNode.Attributes["href"].Value;
                var text = linkNode.InnerText;
                var readCount = item.QuerySelector("div.folTypPost > ul > li > b").InnerText;

                items.Add(new { text, readCount, link });
            }
            // ghi vao file text
            String filepath = "D:\\tet3.txt";
            FileStream fs = new FileStream(filepath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs, Encoding.UTF8);

            foreach (var item in items)
            {
                streamWriter.WriteLine(item);
            }
            streamWriter.Flush();
            fs.Close();
        }
    }
}
