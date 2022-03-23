using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Console.Util
{
    public static class Constants
    {
        public static class Token
        {
            public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EZNEW.Code.GuidHelper.GetGuidUniqueCode(16)));
        }

        public static class RSA
        {
            /// <summary>
            /// 公钥
            /// </summary>
            public const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjCyp6qhEji/0uyL/gZrvC1ZM9a/97/oSpRx7YUHaFlZroafcQlawurmv+SYEMXL/Os8j5QHayro9LInr/jut3iKiWo/lx2xx55eNoh4954WTKRXcrmz5sEHvyPFddxbVTGqzRMhwp4m1Dby2a6rp/rt88h6v1GPE3/GrP42uVKrNpdMsATTiPgpPQSivonKdRD9I6qeZa1EYu8MfArpY0JpMxL3NFCJPGa0x7MoKI4e4SYoKwokfPKlxtccKojh4jhCcS9UpEv6gC/Qe8ipK6IFOqPfcfAdh90qy78h+GTuwvgnN7YNGBmpSyeVH69FycEWNJTS39lynpsgcLCckEQIDAQAB";
            /// <summary>
            /// 私钥
            /// </summary>
            public const string PrivateKey = "MIIEowIBAAKCAQEAjCyp6qhEji/0uyL/gZrvC1ZM9a/97/oSpRx7YUHaFlZroafcQlawurmv+SYEMXL/Os8j5QHayro9LInr/jut3iKiWo/lx2xx55eNoh4954WTKRXcrmz5sEHvyPFddxbVTGqzRMhwp4m1Dby2a6rp/rt88h6v1GPE3/GrP42uVKrNpdMsATTiPgpPQSivonKdRD9I6qeZa1EYu8MfArpY0JpMxL3NFCJPGa0x7MoKI4e4SYoKwokfPKlxtccKojh4jhCcS9UpEv6gC/Qe8ipK6IFOqPfcfAdh90qy78h+GTuwvgnN7YNGBmpSyeVH69FycEWNJTS39lynpsgcLCckEQIDAQABAoIBAF04Vsf3n5/vxJGvdLx8jPy9J/E8WWR+qmejyWmkoaS5VFrth02W2XFUi/LllWRNVe+GUhi6YwbvVD+KeadZiQdxgQc5K1a0iAu8fjSSaBjQfE2WfGyDjInrT8wSuFW6mZ94VkAoDMNekUnRdn+j6nm9thpOziAxOXy0+24IiydCcAR2M/zXpzgmFhC3+YoLalcDF9k10wye78YpjzYDXD9RBftBplViIBftEbPLI7FkJES6XHvabO39nTyUegCbrPd8M70R10PpAqnvgQ3JBMJUQeDoDJJQC4qgArlP+5piE9GTtSI8UB7ucHo0b5WrPHxXF3IY+VHx2ZdGPg4J4QECgYEA4nZ4i4/YK3HDF0EIBYs54Kt3bWvIXtsftb1RjVhCMu6P+FHgq4vLIln80uUxrTdllCAp2eVEBcCob8WLtkEdKZCkrnLbDR5Pagkv7jOdAAyMFVO4BTf7uFl0NRmy5hKluoXaXP9K7ObB5T9jgwiFP4t0fXJ9feEwRNrq2GO6Q1cCgYEAnnUM2yl+Vbfmhj7Syggn431yn+8w6zrxID0StlIFAofuGtxzrT0bdzcnSum6bvygssUzozNoknsAkNI+BC9bst8bOpMGBOWlZgRZ7djOl9sPIzeueDEU4plya0R+ij5sVuchfiWQxNofIJ12PwB2MejWNTxkpVfVSw9XsE3UWtcCgYEA14Tr39X7Yg0YH29SVQRVBo3oUIg83pknKmyLyjpudc430ZkKeffGUGDXjVV122VXFIsviSD5hYf+RLD57280c7QmJadE9M0dwsoPhWA1UDCguEM+ZCkNNDpdr5NPgEnK0yJD2DFVevN5H9UdvTxEo17yc9ibuUqwBzdYPKtcPm0CgYAIfS7WIu/wuaxw7rauOieg6VyVsuPFBmBaoMr614QLMasQ51SPKGHrThodyk3BmmkliqkMijmE92/9UjYeVUctWwSDa9L7ui3VPFBxDJdszUCPW3FOYRrEVQIrOv0ofNWjBjjmIC8UTLc0MRtfnq+2Vsn7CiQ+M2lq3wSMnm7M+wKBgHHWsyHwShThFxfvwxKfhuUo5ePRi/4oS0+7bpIBouyrxcYTkLG5B/bKUj3K6Gf08LIDyrejvUs3eecBv4FWeIg9daJCCHjAvns1OTegC0smWoFOxxa7/02ojYFm0P+gWHvHMtKWxQi0lOBHfYbeboggEf1kEuMgkrSMTTorHaBI";
        }
    }
}
