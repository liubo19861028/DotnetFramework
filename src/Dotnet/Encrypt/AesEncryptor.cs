﻿using Dotnet.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;
namespace Dotnet.Encrypt
{
    public class AesEncryptor
    {
        private readonly byte[] _key = { 0x0F, 0x0E, 0x0D, 0x0C, 0x0B, 0x0A, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00 };
        private readonly byte[] _iv = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public CipherMode Mode { get; set; } = CipherMode.CBC;
        public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;
        public AesEncryptor() : this("")
        {

        }
        public AesEncryptor(string key, string iv)
        {
            if (!key.IsNullOrWhiteSpace())
            {
                _key = Convert.FromBase64String(key);
            }
            if (!iv.IsNullOrWhiteSpace())
            {
                _iv = Convert.FromBase64String(iv);
            }
        }

        public AesEncryptor(string key, byte[] iv)
        {
            if (!key.IsNullOrWhiteSpace())
            {
                _key = Convert.FromBase64String(key);
            }
            _iv = iv;
        }

        public AesEncryptor(string key) : this(key, "")
        {

        }

        /// <summary>加密
        /// </summary>
        public byte[] Encrypt(byte[] dataBytes)
        {
            return EncryptBytes(dataBytes);
        }
        /// <summary>加密
        /// </summary>
        public string Encrypt(string data, string encode = "utf-8")
        {
            var dataBytes = Encoding.GetEncoding(encode).GetBytes(data);
            var encryptedData = EncryptBytes(dataBytes);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>解密
        /// </summary>
        public byte[] Decrypt(byte[] dataBytes)
        {
            return DecryptBytes(dataBytes);
        }

        /// <summary>解密
        /// </summary>
        public string Decrypt(string data, string encode = "utf-8")
        {
            var dataBytes = Convert.FromBase64String(data);
            var dataSource = DecryptBytes(dataBytes);
            return Encoding.GetEncoding(encode).GetString(dataSource);
        }


        /// <summary>加密核心
        /// </summary>
        private byte[] EncryptBytes(byte[] dataBytes)
        {
            var aes = Aes.Create();
            //Mode
            aes.Mode = Mode;
            //Padding
            aes.Padding = Padding;
            var transform = aes.CreateEncryptor(_key, _iv);
            var bytes = transform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
            aes.Dispose();
            return bytes;
        }

        /// <summary>解密核心
        /// </summary>
        private byte[] DecryptBytes(byte[] dataBytes)
        {
            var aes = Aes.Create();
            //Mode
            aes.Mode = Mode;
            //Padding
            aes.Padding = Padding;
            var transform = aes.CreateDecryptor(_key, _iv);
            var bytes = transform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
            aes.Dispose();
            return bytes;
        }

    }
}
