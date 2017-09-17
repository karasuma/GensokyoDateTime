using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowolf
{
	/// <summary>
	/// 幻想郷の暦、時刻（十二時辰）として表現される瞬間を表します。
	/// </summary>
	public class GensokyoDateTime
	{
		/// <summary>
		/// 三精
		/// </summary>
		private readonly string[] _sansei = new[]{ "日", "月", "星" };

		/// <summary>
		/// 四季
		/// </summary>
		private readonly string[] _siki = new[]{ "春", "夏", "秋", "冬" };

		/// <summary>
		/// 五行
		/// </summary>
		private readonly string[] _gogyo = new[]{ "土", "火", "水", "木", "金" };

		/// <summary>
		/// 旧暦月
		/// </summary>
		private readonly string[] _kyurekizuki = new[]{ "睦月", "如月", "弥生", "卯月", "皐月", "水無月", "文月", "葉月", "長月", "神無月", "霜月", "師走" };

		/// <summary>
		/// 十二支
		/// </summary>
		private readonly string[] _junishi = new[]{ "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

		/// <summary>
		/// ?つ刻
		/// </summary>
		private readonly string[] _koku = new[]{ "一つ", "二つ", "三つ", "四つ" };

		/// <summary>
		/// 正刻の鐘
		/// </summary>
		private readonly string[] _seikoku = new[]{ "夜九つ", "夜八つ", "暁七つ", "明六つ", "朝五つ", "昼四つ", "昼九つ", "昼八つ", "夕七つ", "暮六つ", "宵五つ", "夜四つ" };

		/// <summary>
		/// DateTimeを取得および設定します。
		/// </summary>
		public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
		private DateTime _dateTime;

		/// <summary>
		/// 季を取得します。
		/// 仮に現在が2005年の場合、120が返されます。
		/// 季が0以下の場合、例外をスローします。
		/// </summary>
		public int Ki
		{
			get
			{
				var ki = DateTime.Year - 1885;
				if( ki < 0 )
				{
					throw new InvalidOperationException( "博麗大結界の出現以前の年が指定されています。" );
				}
				return ki;
			}
		}

		/// <summary>
		/// ここが出雲であるかを取得および設定します。
		/// この設定は、旧暦月名に関係します。
		/// </summary>
		public bool IsIzumo { get { return _isIzumo; } set { _isIzumo = value; } }
		private bool _isIzumo = false;

		/// <summary>
		/// 三精を取得します。
		/// </summary>
		public string Sansei { get { return _sansei[Ki % 3]; } }

		/// <summary>
		/// 四季を取得します。
		/// </summary>
		public string Siki { get { return _siki[Ki % 4]; } }

		/// <summary>
		/// 五行を取得します。
		/// </summary>
		public string Gogyo { get { return _gogyo[Ki % 5]; } }

		/// <summary>
		/// 旧暦月名を取得します。
		/// </summary>
		public string Kyurekizuki
		{
			get
			{
				if( IsIzumo && DateTime.Month == 10 )
				{
					return "神在月";
				}
				return _kyurekizuki[DateTime.Month - 1];
			}
		}

		/// <summary>
		/// 十二支を取得します。
		/// </summary>
		public string Junishi { get { return _junishi[GetQuarterTimeIndex() / 4]; } }

		/// <summary>
		/// DateTimeが今何刻かを取得します。
		/// </summary>
		public string Koku { get { return _koku[GetQuarterTimeIndex() % 4]; } }

		/// <summary>
		/// 正刻の鐘を取得します。
		/// </summary>
		public string Seikoku { get { return _seikoku[GetQuarterTimeIndex() / 4]; } }

		public GensokyoDateTime(DateTime datetime)
		{
			_dateTime = datetime;
		}

		public GensokyoDateTime(int year, int month, int day)
			: this( new DateTime( year, month, day ) ) { }
		public GensokyoDateTime(int year, int month, int day, int hour, int minute)
			: this( new DateTime( year, month, day, hour, minute, 0 ) ) { }

		/// <summary>
		/// 十二時辰のインデックスを30分毎に取得（23:00（子）～）
		/// </summary>
		/// <returns></returns>
		private int GetQuarterTimeIndex()
		{
			var minuteAdjustment = DateTime.Minute < 30 ? 0 : 1;
			var hour = DateTime.Hour;

			var index = 0;
			if( hour == 23 )
				index = 0 + minuteAdjustment;
			else
				index = (hour + 1) * 2 + minuteAdjustment;

			return index;
		}
	}
}
