using Dotnet.Encrypt;
using Dotnet.Parameters;

namespace Dotnet.Signatures {
    /// <summary>
    /// 签名服务
    /// </summary>
    public class SignManager : ISignManager {
        /// <summary>
        /// 签名密钥
        /// </summary>
        private readonly ISignKey _key;
        /// <summary>
        /// Url参数生成器
        /// </summary>
        private readonly UrlParameterBuilder _builder;

        /// <summary>
        /// 初始化签名服务
        /// </summary>
        /// <param name="key">签名密钥</param>
        /// <param name="builder">Url参数生成器</param>
        public SignManager( ISignKey key, UrlParameterBuilder builder = null ) {
            //key.CheckNull( nameof( key ) );
            _key = key;
            _builder = builder == null ? new UrlParameterBuilder() : new UrlParameterBuilder( builder );
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public ISignManager Add( string key, object value ) {
            _builder.Add( key, value );
            return this;
        }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign() {
            RsaEncryptor rsaEncryptor = new RsaEncryptor().LoadPrivateKey(_key.GetKey());

            return rsaEncryptor.SignData( _builder.Result( true ));
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sign">签名</param>
        public bool Verify( string sign ) {
            RsaEncryptor rsaEncryptor = new RsaEncryptor().LoadPrivateKey(_key.GetKey());
            return rsaEncryptor.VerifyData(_builder.Result(true), sign);
           // return Encrypt.Rsa2Verify( _builder.Result( true ), _key.GetPublicKey(), sign );
        }
    }
}
