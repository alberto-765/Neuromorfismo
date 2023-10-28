using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.ServicesDependencies;

namespace WebMedicina.BackEnd.Service {
    public class Encriptador : IEncriptador {
        private readonly byte[] key = new byte[32]; // clave de 32 bytes
        private readonly byte[] vector = new byte[16]; // vector de inicializacion aleatorio

        // Generamos key y vector de inicializacion aleatoriamente
        public Encriptador() {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(key);
                rng.GetBytes(vector);
            }
        }
        public string Desencriptar(string claveEncriptada) {
			try {
                // Creamos el aes para crear el desencriptador
                using (Aes aesAlg = Aes.Create()) {
                    // Asignamos clave y vector de inicializacion aleatorios
                    aesAlg.Key = key;
                    aesAlg.IV = vector;

                    // Creamos el desencriptador
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Creamos un memoryStream para escribir byte a byte la nueva clave descencriptada
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(claveEncriptada))) {

                        // Creamos operador de cifrado con la configuracion del desencriptador 
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {

                            // Creamos streamreader para leer los bytes que genera el operador de cifrado
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {

                                // Devolvemos la clave desencriptada
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            } catch (Exception) {

				throw;
			}       
        }

        public string Encriptar(string claveDesencriptada) {
            try {
                using (Aes aesAlg = Aes.Create()) {
                    aesAlg.Key = key;
                    aesAlg.IV = vector;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Creamos un memoryStream para escribir byte a byte la nueva clave encriptada
                    using (MemoryStream msEncrypt = new MemoryStream()) {

                        // Creamos operador de cifrado con la configuracion del encriptador 
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {

                            // Creamos streamreader para escribir los bytes que genera el operador de cifrado
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                                swEncrypt.Write(claveDesencriptada);
                            }
                        }

                        // Devolvemos la clave encriptada convertida desde bytes a string
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            } catch (Exception) {

                throw;
            }
        }
    }
}
