// Helpers/Settings.cs
using App7.ServiceReference1;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace App7
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string NeedSyncFeedbackKey = "need_sync_feedback";
        private static readonly bool NeedSyncFeedbackDefault = false;

        private const string LastSyncKey = "last_sync";
        private static readonly DateTime LastSyncDefault = DateTime.Now.AddDays(-30);

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;

        private const string UserPassKey = "userpass_key";
        private static readonly string UserPassDefault = string.Empty;

        private const string UserTipoKey = "usertipo_key";
        private static readonly string UserTipoDefault = string.Empty;

        private const string FaceIdKey = "faceId_key";
        private static readonly string FaceIdDefault = string.Empty;

        private const string SemIni2Key = "semIni2_key";
        private static readonly string SemIni2Default = string.Empty;

        private const string SomeIntKey = "int_key";
        private static readonly int SomeIntDefault = 6251986;

        private static Usuario user;
        private static Proveedor provee;
        #endregion

#if DEBUG
        //always refresh in debug
        public static bool NeedsSync
        {
            get { return true; }
        }
#else
		public static bool NeedsSync
		{
			get { return LastSync < DateTime.Now.AddDays (-3); }
		}
#endif

        public static DateTime LastSync
        {
            get
            {
                return AppSettings.GetValueOrDefault<DateTime>(LastSyncKey, LastSyncDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<DateTime>(LastSyncKey, value);
            }
        }

        public static bool NeedSyncFeedback
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(NeedSyncFeedbackKey, NeedSyncFeedbackDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(NeedSyncFeedbackKey, value);
            }
        }

        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value); }
        }

        public static string UserPass
        {
            get { return AppSettings.GetValueOrDefault<string>(UserPassKey, UserPassDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserPassKey, value); }
        }

        public static string UserTipo
        {
            get { return AppSettings.GetValueOrDefault<string>(UserTipoKey, UserTipoDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserTipoKey, value); }
        }

        public static string FaceId
        {
            get { return AppSettings.GetValueOrDefault<string>(FaceIdKey, FaceIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(FaceIdKey, value); }
        }

        public static string SemIni2
        {
            get { return AppSettings.GetValueOrDefault<string>(SemIni2Key, SemIni2Default); }
            set { AppSettings.AddOrUpdateValue<string>(SemIni2Key, value); }
        }

        public static int SomeInt
        {
            get { return AppSettings.GetValueOrDefault<int>(SomeIntKey, SomeIntDefault); }
            set { AppSettings.AddOrUpdateValue<int>(SomeIntKey, value); }
        }

        public static Usuario User
        {
            get
            {
                if (user == null)
                    return new Usuario { Nick = "Usuario", Tipo = "U" };
                else
                    return user;
            }
            set
            {
                user = value;
            }
        }

        public static Proveedor Provee
        {
            get
            {
                if (provee == null)
                    return new Proveedor { };
                else
                    return provee;
            }
            set
            {
                provee = value;
            }
        }
    }
}