using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta_Api.ResponseModel {
    public class ApiWrapperResponse<T> {
        public T ResponseValue { get; private set; }
        public int ResponseCode { get; private set; }

        public string  MessageResponse { get; private set; }
        public ApiWrapperResponse(T value, int response) {
            this.ResponseValue = value;
            this.ResponseCode = response;
            MessageResponse = "OK";
        }

        public ApiWrapperResponse(int response, string messageResponse= "Unexpected Error try later.") {
            this.ResponseValue = default(T);
            this.ResponseCode = response;
            MessageResponse = MessageResponse;
        }
        public override string ToString() {
            return $"[ ResponseCode = {ResponseCode} , MessageResponse =  {MessageResponse}, ResponseValue =  {string_helper(ResponseValue)}]";
        }

        private string string_helper(T obj) {
         
            return obj == null ? "Null" : ( obj.GetType().IsArray? "Array": obj.ToString() );
        }
    }
}
