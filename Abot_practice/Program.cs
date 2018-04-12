using System;
using System.IO;
using Abot.Crawler;
using Abot.Poco;

namespace AbotCrawler
{
    // Nuget: Install-Package Abot

    class Program
    {
        static void Main(string[] args)
        {
            // 크롤러 인스턴스 생성
            IWebCrawler crawler = new PoliteWebCrawler();

            // 옵션과 함께 크롤러 인스턴스 생성할 경우
            // var crawlConfig = new CrawlConfiguration();
            // crawlConfig.CrawlTimeoutSeconds = 1000;
            // crawlConfig.MaxConcurrentThreads = 10;
            // crawlConfig.MaxPagesToCrawl = 10;
            // crawlConfig.UserAgentString = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0";
            // IWebCrawler crawler = new PoliteWebCrawler(crawlConfig);

            // 이벤트 핸들러 셋업
            crawler.PageCrawlStartingAsync += (s, e) =>
            {
                Console.WriteLine("Starting : {0}", e.PageToCrawl);
            };

            crawler.PageCrawlCompletedAsync += (s, e) =>
            {
                CrawledPage pg = e.CrawledPage;

                string fn = pg.Uri.Segments[pg.Uri.Segments.Length - 1];
                File.WriteAllText(fn, pg.Content.Text);

                //var hdoc = pg.HtmlDocument; //HtmlAgilityPack HtmlDocument

                Console.WriteLine("Completed : {0}", pg.Uri.AbsoluteUri);
            };

            // 크롤 시작
            string siteUrl = "http://www.naver.com";
            Uri uri = new Uri(siteUrl);

            crawler.Crawl(uri);
        }

    }
}
 
 
