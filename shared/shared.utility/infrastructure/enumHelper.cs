﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace shared.utility.infrastructure {
    public static class EnumHelper<T> {
        public static IList<T> GetValues(Enum value) {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => (T)Enum.Parse(value.GetType(), fi.Name, false)).ToList();
        }

        public static T Parse(string value) {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value) {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value) {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value) {
            try {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if(descriptionAttributes == null)
                    return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch {
                return "مقدار غیرمجاز";
            }
        }
    }
}
