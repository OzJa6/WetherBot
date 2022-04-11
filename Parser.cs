using AngleSharp;

namespace Parserapi // Note: actual namespace depends on the project name.
{
    public class Parser
    {
        public static async Task parseAsync()
        {
            var html = @"
<html>
<head>
<title>My Title</title>
</head>
<body>
<h1>Heading I</h1>
<p>Paragraph I</p>
<p>Paragraph II</p>
<p>Paragraph III</p>
</body>
</html>";

            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc = await context.OpenAsync(req => req.Content(html));
            var collection = doc.Body.GetElementsByTagName("p");

            Console.WriteLine(doc.Title);
            Console.WriteLine(doc.Body.InnerHtml.Trim());
            Console.WriteLine(collection[0].InnerHtml.Trim());
        }

        public static async Task UsingLinq()
        {
            //Create a new context for evaluating webpages with the default config
            var context = BrowsingContext.New(Configuration.Default);

            //Create a document from a virtual request / response pattern
            var document = await context.OpenAsync(req => req.Content(String.Format("<ul><li>First item<li>Second item<li class='blue'>Third item!<li class='blue red'>Last item!</ul>")));

            //Do something with LINQ
            var blueListItemsLinq = document.All.Where(m => m.LocalName == "li" && m.ClassList.Contains("blue"));

            //Or directly with CSS selectors
            var blueListItemsCssSelector = document.QuerySelectorAll("li.blue");

            Console.WriteLine("Comparing both ways ...");

            Console.WriteLine();
            Console.WriteLine("LINQ:");

            foreach (var item in blueListItemsLinq)
            {
                Console.WriteLine(item.TextContent);
            }

            Console.WriteLine();
            Console.WriteLine("CSS:");

            foreach (var item in blueListItemsCssSelector)
            {
                Console.WriteLine(item.TextContent);
            }
        }
    }
}