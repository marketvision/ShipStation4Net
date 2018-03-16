#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;

namespace ShipStation4Net.Converters
{
	public class SpecificTimeZoneDateConverter : DateTimeConverterBase
	{
		private TimeZoneInfo _timeZoneInfo;
		private string _dateFormat;

		public SpecificTimeZoneDateConverter(string dateFormat, TimeZoneInfo timeZoneInfo)
		{
			_dateFormat = dateFormat;
			_timeZoneInfo = timeZoneInfo;
		}
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime)
				|| objectType == typeof(DateTime?);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			var value = DateTime.Parse(Convert.ToString(reader.Value));
			return ConvertTimeWithAmbiguousTimeHandling(value, _timeZoneInfo, TimeZoneInfo.Utc);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var val = value is DateTime ? (DateTime)value : Convert.ToDateTime(value);

			if (val.Kind == DateTimeKind.Unspecified)
			{
				val = DateTime.SpecifyKind(val, DateTimeKind.Local);
			}

			var sourceTimeZone = val.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : TimeZoneInfo.Local;
			val = ConvertTimeWithAmbiguousTimeHandling(val, sourceTimeZone, _timeZoneInfo);

			writer.WriteValue(val.ToString(_dateFormat));
			writer.Flush();
		}

		private static DateTime ConvertTimeWithAmbiguousTimeHandling(DateTime val, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			if (!sourceTimeZone.IsInvalidTime(val))
			{
				return TimeZoneInfo.ConvertTime(val, sourceTimeZone, destinationTimeZone);
			}

			//because ShipStation operates in PST/PDT: it's still possible to have ambiguous datetime even if client sends/receives data in UTC
			var ambiguousTimeRule = sourceTimeZone.GetAdjustmentRules()
				.First(x => x.DateStart <= val && val <= x.DateEnd);

			val = val.Add(new TimeSpan(-ambiguousTimeRule.DaylightDelta.Ticks));
			val = TimeZoneInfo.ConvertTime(val, sourceTimeZone, destinationTimeZone);
			return val.Add(ambiguousTimeRule.DaylightDelta);
		}
	}
}
