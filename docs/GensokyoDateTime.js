var GensokyoDateTime = (function ()
{
	// 三精
	var _sansei = ["日", "月", "星"];
	// 四季
	var _siki = ["春", "夏", "秋", "冬"];
	// 五行
	var _gogyo = ["土", "火", "水", "木", "金"];
	// 旧暦月
	var _kyurekizuki = ["睦月", "如月", "弥生", "卯月", "皐月", "水無月", "文月", "葉月", "長月", "神無月", "霜月", "師走"];
	// 十二支
	var _junishi = ["子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥"];
	// ?つ刻
	var _koku = ["一つ", "二つ", "三つ", "四つ"];
	// 正刻の鐘
	var _seikoku = ["夜九つ", "夜八つ", "暁七つ", "明六つ", "朝五つ", "昼四つ", "昼九つ", "昼八つ", "夕七つ", "暮六つ", "宵五つ", "夜四つ"];

	var GensokyoDateTime = function (date)
	{
		if (!(this instanceof GensokyoDateTime))
		{
			return new GensokyoDateTime(date);
		}

		this.date = date;
		this.isIzmo = false;
	}

	var prototype = GensokyoDateTime.prototype;

	// 季の取得
	prototype.getKi = function ()
	{
		var ki = this.date.getFullYear() - 1885;
		if (ki < 0)
		{
			throw new Error("博麗大結界の出現以前の年が指定されています。");
		}
		return ki;
	}

	// 三精の取得
	prototype.getSansei = function ()
	{
		return _sansei[this.getKi() % 3];
	}

	// 四季の取得
	prototype.getSiki = function ()
	{
		return _siki[this.getKi() % 4];
	}

	// 五行の取得
	prototype.getGogyo = function ()
	{
		return _gogyo[this.getKi() % 5];
	}

	// 旧暦月名を取得
	prototype.getKyurekizuki = function ()
	{
		if (this.isIzmo == true && this.date.getMonth() + 1 == 10)
		{
			return "神在月";
		}
		return _kyurekizuki[this.date.getMonth()];
	}

	// 十二時辰のインデックスを30分毎に取得（23:00（子）～）
	prototype._getQuarterTimeIndex = function ()
	{
		var minuteAdjustment = 1;
		if (this.date.getMinutes() < 30)
		{
			minuteAdjustment = 0;
		}
		var hour = this.date.getHours();

		var index = 0;
		if (hour == 23)
		{
			index = 0 + minuteAdjustment;
		}
		else
		{
			index = (hour + 1) * 2 + minuteAdjustment;
		}
		return index;
	}

	// 十二支を取得
	prototype.getJunishi = function ()
	{
		return _junishi[~~(this._getQuarterTimeIndex() / 4)];
	}

	// 今何刻かを取得
	prototype.getKoku = function ()
	{
		return _koku[this._getQuarterTimeIndex() % 4];
	}

	// 正刻の鐘
	prototype.getSeikoku = function ()
	{
		return _seikoku[~~(this._getQuarterTimeIndex() / 4)];
	}

	return GensokyoDateTime;
})();