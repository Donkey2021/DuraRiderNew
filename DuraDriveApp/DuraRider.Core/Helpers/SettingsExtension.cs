using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DuraRider.Core.Helpers
{
    public class SettingsExtension
    {
        public static string GetStringForKey(string key)
        {
            var value = Preferences.Get(key, string.Empty);
            return value;
        }
        public static void AddOrUpdateStringForKey(string key, string value)
        {
            try
            {
                Preferences.Set(key, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving settings value: {ex}");
            }
        }
        public static T GetClassForKey<T>(string key, T @default) where T : class
        {
            string serialized = Preferences.Get(key, string.Empty);
            T result = @default;

            try
            {
                JsonSerializerSettings serializeSettings = GetSerializerSettings();
                result = JsonConvert.DeserializeObject<T>(serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deserializing settings value: {ex}");
            }
            return result;
        }


        public static void AddClassForKey<T>(string key, T obj) where T : class
        {
            try
            {
                JsonSerializerSettings serializeSettings = GetSerializerSettings();
                string serialized = JsonConvert.SerializeObject(obj, serializeSettings);
                Preferences.Set(key, serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }

        public static void DeleteClassForKey(string key)
        {
            try
            {
                Preferences.Remove(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }
        public static void DeletePreferenceKey(string key)
        {
            try
            {
                Preferences.Remove(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static string UserEmail
        {
            get => Preferences.Get(nameof(UserEmail), string.Empty);
            set => Preferences.Set(nameof(UserEmail), value);
        }
        public static string User
        {
            get => Preferences.Get(nameof(User), string.Empty);
            set => Preferences.Set(nameof(User), value);
        }
        public static string Phone
        {
            get => Preferences.Get(nameof(Phone), string.Empty);
            set => Preferences.Set(nameof(Phone), value);
        }
        public static string Country
        {
            get => Preferences.Get(nameof(Country), string.Empty);
            set => Preferences.Set(nameof(Country), value);
        }
        public static string Gender
        {
            get => Preferences.Get(nameof(Gender), string.Empty);
            set => Preferences.Set(nameof(Gender), value);
        }
        public static int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }
        public static int Age
        {
            get => Preferences.Get(nameof(Age), 0);
            set => Preferences.Set(nameof(Age), value);
        }
        public static int MyPoints
        {
            get => Preferences.Get(nameof(MyPoints), 0);
            set => Preferences.Set(nameof(MyPoints), value);
        }
        public static string UserType

        {
            get => Preferences.Get(nameof(UserType), string.Empty);
            set => Preferences.Set(nameof(UserType), value);
        }
        public static string Preferedlanguage

        {
            get => Preferences.Get(nameof(Preferedlanguage), string.Empty);
            set => Preferences.Set(nameof(Preferedlanguage), value);
        }
        public static string AppName
        {
            get => Preferences.Get(nameof(AppName), string.Empty);
            set => Preferences.Set(nameof(AppName), value);
        }

        public static string SelectedLanguage
        {
            get => Preferences.Get(nameof(SelectedLanguage), string.Empty);
            set => Preferences.Set(nameof(SelectedLanguage), value);
        }
        public static string SelectedUserType
        {
            get => Preferences.Get(nameof(SelectedLanguage), string.Empty);
            set => Preferences.Set(nameof(SelectedLanguage), value);
        }
        public static string CurrentUserCulture
        {
            get => Preferences.Get(nameof(CurrentUserCulture), string.Empty);
            set => Preferences.Set(nameof(CurrentUserCulture), value);
        }
       

        public const string AppDbSecret = "TechMedDBSecret";
        public SettingsExtension()
        {
        }
    }
}
