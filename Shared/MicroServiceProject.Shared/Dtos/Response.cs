﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MicroServiceProject.Shared.Dtos
{

    public class Response<T>
    {
        public T Data { get; set; }//giriş Başarılımı değil mi
        [JsonIgnore]
        public int StatusCode { get; set; }//aktif-paasif mi 
        [JsonIgnore]
        public bool IsSuccessful { get; set; }//
        public List<string> Errors { get; set; }//birden fazla hata dönmesi durumu

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { StatusCode = statusCode, Data = default(T), IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode, T data)
        {
            return new Response<T> { StatusCode = statusCode, Data = data, IsSuccessful = true };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}