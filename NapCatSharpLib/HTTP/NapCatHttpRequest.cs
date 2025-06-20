﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NapCatSharpLib.HTTP
{
    public class NapCatHttpRequest
    {
        public async Task<T> APIHttpUrlAsync<T>(string url, string method, string attr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            request.Method = method;
            request.Timeout = 1000000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (method != "GET")
            {
                byte[] data = Encoding.UTF8.GetBytes(attr);
                request.ContentLength = data.Length;
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream streamRes = response.GetResponseStream();
                    string result = new StreamReader(streamRes, Encoding.UTF8).ReadToEnd();

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Parsing JSON Respond : {e.Message} \n {e.StackTrace}");
                        return default(T);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<T> APIHttpJsonCustomHeaderAsync<T>(string url, string method, string attr, WebHeaderCollection headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            request.Headers = headers;
            request.Method = method;
            request.Timeout = 1000000;
            request.ContentType = "application/json";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            byte[] data = Encoding.UTF8.GetBytes(attr);
            request.ContentLength = data.Length;
            Stream stream = await request.GetRequestStreamAsync();
            stream.Write(data, 0, data.Length);
            stream.Close();

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream streamRes = response.GetResponseStream();
                    string result = new StreamReader(streamRes, Encoding.UTF8).ReadToEnd();

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Parsing JSON Respond : {e.Message} \n {e.StackTrace}");
                        return default(T);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<T> APIHttpUrlCustomHeaderAsync<T>(string url, string method, string attr, WebHeaderCollection headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            request.Headers = headers;
            request.Method = method;
            request.Timeout = 1000000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (method != "GET")
            {
                byte[] data = Encoding.UTF8.GetBytes(attr);
                request.ContentLength = data.Length;
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream streamRes = response.GetResponseStream();
                    string result = new StreamReader(streamRes, Encoding.UTF8).ReadToEnd();

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Parsing JSON Respond : {e.Message} \n {e.StackTrace}");
                        return default(T);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<string> APIHttpUrlAsync(string url, string method, string attr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            request.Method = method;
            request.Timeout = 1000000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (method != "GET")
            {
                byte[] data = Encoding.UTF8.GetBytes(attr);
                request.ContentLength = data.Length;
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream streamRes = response.GetResponseStream();
                    string result = new StreamReader(streamRes, Encoding.UTF8).ReadToEnd();

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<T> APIHttpJsonAsync<T>(string url, string method, string json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(url));
            request.Method = method;
            request.Timeout = 1000000;
            request.ContentType = "application/json";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            byte[] data = Encoding.UTF8.GetBytes(json);
            request.ContentLength = data.Length;
            Stream stream = await request.GetRequestStreamAsync();
            stream.Write(data, 0, data.Length);
            stream.Close();

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream streamRes = response.GetResponseStream();
                    string result = new StreamReader(streamRes, Encoding.UTF8).ReadToEnd();

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Parsing JSON Respond : {e.Message} \n {e.StackTrace}");
                        return default(T);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }
    }
}
