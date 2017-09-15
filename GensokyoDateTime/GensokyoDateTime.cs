﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowolf
{
	/// <summary>
	/// 幻想郷の暦、時刻として表現される瞬間を表します。
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
		/// このインスタンスのDateTimeを取得および設定します。
		/// </summary>
		public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
		private DateTime _dateTime;

		/// <summary>
		/// このインスタンスの季を取得します。
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
		/// このインスタンスの三精を取得します。
		/// </summary>
		public string Sansei { get { return _sansei[Ki % 3]; } }

		/// <summary>
		/// このインスタンスの四季を取得します。
		/// </summary>
		public string Siki { get { return _siki[Ki % 4]; } }

		/// <summary>
		/// このインスタンスの五行を取得します。
		/// </summary>
		public string Gogyo { get { return _gogyo[Ki % 5]; } }

		/// <summary>
		/// このインスタンスの旧暦月名を取得します。
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

		public GensokyoDateTime(DateTime datetime)
		{
			_dateTime = datetime;
		}

		public GensokyoDateTime(int year, int month, int day)
			: this( new DateTime( year, month, day ) ) { }
		public GensokyoDateTime(int year, int month, int day, int hour, int minute, int second)
			: this( new DateTime( year, month, day, hour, minute, second ) ) { }
	}
}
