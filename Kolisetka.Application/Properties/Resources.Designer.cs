﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kolisetka.Application.Properties {
    using System;
    
    
    /// <summary>
    ///   Klasa zasobu wymagająca zdefiniowania typu do wyszukiwania zlokalizowanych ciągów itd.
    /// </summary>
    // Ta klasa została automatycznie wygenerowana za pomocą klasy StronglyTypedResourceBuilder
    // przez narzędzie, takie jak ResGen lub Visual Studio.
    // Aby dodać lub usunąć składową, edytuj plik ResX, a następnie ponownie uruchom narzędzie ResGen
    // z opcją /str lub ponownie utwórz projekt VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        /// Zwraca buforowane wystąpienie ResourceManager używane przez tę klasę.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Kolisetka.Application.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Przesłania właściwość CurrentUICulture bieżącego wątku dla wszystkich
        ///   przypadków przeszukiwania zasobów za pomocą tej klasy zasobów wymagającej zdefiniowania typu.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product creation failed..
        /// </summary>
        public static string Product_Creation_Failure {
            get {
                return ResourceManager.GetString("Product.Creation.Failure", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product creation successful..
        /// </summary>
        public static string Product_Creation_Success {
            get {
                return ResourceManager.GetString("Product.Creation.Success", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product deletion failed..
        /// </summary>
        public static string Product_Deletion_Failure {
            get {
                return ResourceManager.GetString("Product.Deletion.Failure", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product deletion successful..
        /// </summary>
        public static string Product_Deletion_Success {
            get {
                return ResourceManager.GetString("Product.Deletion.Success", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product update failed..
        /// </summary>
        public static string Product_Update_Failure {
            get {
                return ResourceManager.GetString("Product.Update.Failure", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Product update successful..
        /// </summary>
        public static string Product_Update_Success {
            get {
                return ResourceManager.GetString("Product.Update.Success", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} has to be grater than 0..
        /// </summary>
        public static string Product_Validator_GreaterThan0 {
            get {
                return ResourceManager.GetString("Product.Validator.GreaterThan0", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} has to be included within {PropertyName} enum..
        /// </summary>
        public static string Product_Validator_InvalidEnum {
            get {
                return ResourceManager.GetString("Product.Validator.InvalidEnum", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} is decimal value with scale 2 and precision 7..
        /// </summary>
        public static string Product_Validator_InvalidPrecision {
            get {
                return ResourceManager.GetString("Product.Validator.InvalidPrecision", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} does not exist..
        /// </summary>
        public static string Product_Validator_NotExists {
            get {
                return ResourceManager.GetString("Product.Validator.NotExists", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} is required..
        /// </summary>
        public static string Product_Validator_Required {
            get {
                return ResourceManager.GetString("Product.Validator.Required", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu {PropertyName} must not exceed {MaxLength} characters..
        /// </summary>
        public static string Product_Validator_TooLong {
            get {
                return ResourceManager.GetString("Product.Validator.TooLong", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu User does not exist or invalid password..
        /// </summary>
        public static string User_Validator_NotExistsOrInvalidPassword {
            get {
                return ResourceManager.GetString("User.Validator.NotExistsOrInvalidPassword", resourceCulture);
            }
        }
    }
}
