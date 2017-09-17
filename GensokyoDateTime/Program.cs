using System;
using Crowolf;

class Program
{
	static void Main(string[] args)
	{
		Func<int, string> KanjiNumber = num =>
		{
			var keta = new[]{ "", "十", "百" };
			var ketaNum = new[]{ "", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
			var kanji = "";
			var ketaCount = 0;
			do
			{
				var currentNum = num % 10;

				if(ketaCount == 0)
				{
					kanji = ketaNum[currentNum];
				}
				else
				{
					kanji = keta[ketaCount] + kanji;
					if(currentNum > 1)
					{
						kanji = ketaNum[currentNum] + kanji;
					}
				}

				ketaCount++;
				num /= 10;
			}while(num > 0);
			return kanji;
		};

		var gskdt = new GensokyoDateTime(DateTime.Now);
		Console.WriteLine( string.Format( "第{0}({6})季　{1}と{2}と{3}の年　{4}ノ{5}({7}日)",
			KanjiNumber( gskdt.Ki ), gskdt.Sansei, gskdt.Siki, gskdt.Gogyo, gskdt.Kyurekizuki, KanjiNumber( gskdt.DateTime.Day ), gskdt.Ki, gskdt.DateTime.Day ) );
		Console.WriteLine( string.Format( "{0}:{1} ({2}字、{3}刻、{4})",
			gskdt.DateTime.Hour, gskdt.DateTime.Minute, gskdt.Junishi, gskdt.Koku, gskdt.Seikoku ) );
		Console.ReadKey( true );
	}
}