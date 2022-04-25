using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 通讯录部分接口调用
    /// </summary>
    public class ContactHandler : DingtalkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public ContactHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap.Add("user_create", UserCreateEncode);
            DecoderMap.Add("user_create", UserCreateDecode);
            EncoderMap.Add("department_get", DepartmentGetEncode);
            EncoderMap.Add("department_list", DepartmentListEncode);
            EncoderMap.Add("department_create", DepartmentCreateEncode);
            EncoderMap.Add("get_org_user_count", GetUserCountEncode);
            EncoderMap.Add("get_dept_nember_ids", GetDeptMemberIdsEncode);
            EncoderMap.Add("get_dept_nember_list", GetDeptMemberListEncode);
            EncoderMap.Add("get_dept_nember_simple", GetDeptMemberSimpleEncode);

            DecoderMap.Add("department_get", DepartmentGetDecode);
            DecoderMap.Add("department_list", DepartmentListDecode);
            DecoderMap.Add("department_create", DepartmentCreateDecode);
            DecoderMap.Add("get_org_user_count", GetUserCountDecode);
            DecoderMap.Add("get_dept_nember_ids", GetDeptMemberIdsDecode);
            DecoderMap.Add("get_dept_nember_list", GetDeptMemberListDecode);
            DecoderMap.Add("get_dept_nember_simple", GetDeptMemberSimpleDecode);
        }

        #region UserCreate
        private void UserCreateEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.UserCreateRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.name))
                {
                    ctx.Response = new Error() { errmsg = "missing name" };
                    return;
                }
                if (string.IsNullOrEmpty(req.mobile))
                {
                    ctx.Response = new Error() { errmsg = "missing mobile" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(req);
                ctx.RequestPath = "/user/create"
                    + "?access_token=" + req.access_token;
            }
        }
        private void UserCreateDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.UserCreateResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region DepartmentGet
        private void DepartmentGetEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.DepartmentGetRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/department/get"
                    + "?access_token=" + req.access_token
                    + "&id=" + req.id
                    + "&lang=" + req.lang;
            }
        }
        private void DepartmentGetDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.DepartmentGetResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region DepartmentList
        private void DepartmentListEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.DepartmentListRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                //ctx.HttpRequestString = JsonConvert.SerializeObject(req);
                ctx.RequestPath = "/department/list"
                    + "?access_token=" + req.access_token
                    + "&id=" + req.id
                    + "&lang=" + req.lang
                    + "&fetch_child=" + req.fetch_child;
            }
        }
        private void DepartmentListDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.DepartmentListResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region DepartmentCreate
        private void DepartmentCreateEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.DepartmentCreateRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.name))
                {
                    ctx.Response = new Error() { errmsg = "missing name" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(req);
                ctx.RequestPath = "/department/create"
                    + "?access_token=" + req.access_token;
            }
        }
        private void DepartmentCreateDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.DepartmentCreateResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetUserCount
        private void GetUserCountEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.GetUserCountRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                //ctx.HttpRequestString = JsonConvert.SerializeObject(req);
                ctx.RequestPath = "/user/get_org_user_count"
                    + "?access_token=" + req.access_token
                    + "&onlyActive=" + req.onlyActive;
            }
        }
        private void GetUserCountDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetUserCountResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetDeptMemberIds
        private void GetDeptMemberIdsEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.GetDeptMemberIdsRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.deptId))
                {
                    ctx.Response = new Error() { errmsg = "missing deptId" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                //ctx.HttpRequestString = JsonConvert.SerializeObject(req);
                ctx.RequestPath = "/user/getDeptMember"
                    + "?access_token=" + req.access_token
                    + "&deptId=" + req.deptId;
            }
        }
        private void GetDeptMemberIdsDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetDeptMemberIdsResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetDeptMemberList
        private void GetDeptMemberListEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.GetDeptMemberListRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.department_id))
                {
                    ctx.Response = new Error() { errmsg = "missing department_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/user/listbypage"
                    + "?access_token=" + req.access_token
                    + "&department_id=" + req.department_id
                    + "&size=" + req.size
                    + "&offset=" + req.offset
                    + "&order=" + req.order
                    + "&lang=" + req.lang;
            }
        }
        private void GetDeptMemberListDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetDeptMemberListResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetDeptMemberSimple
        private void GetDeptMemberSimpleEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.GetDeptMemberListRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.department_id))
                {
                    ctx.Response = new Error() { errmsg = "missing department_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/user/simplelist"
                    + "?access_token=" + req.access_token
                    + "&department_id=" + req.department_id
                    + "&size=" + req.size
                    + "&offset=" + req.offset
                    + "&order=" + req.order
                    + "&lang=" + req.lang;
            }
        }
        private void GetDeptMemberSimpleDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetDeptMemberListResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

    }
}