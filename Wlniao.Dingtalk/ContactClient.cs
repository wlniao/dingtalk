using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wlniao.Dingtalk.Request;
using Wlniao.Dingtalk.Response;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 通讯录访问
    /// </summary>
    public class ContactClient : Client
    {
        /// <summary>
        /// 
        /// </summary>
        public ContactClient()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            handler = new ApiHandler();
            handler = new ContactHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ContactClient(String AppKey, String AppSecret)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            handler = new ApiHandler();
            handler = new ContactHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ContactClient(String CorpId, String AppKey, String AppSecret, String SuiteTicket)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            handler = new ApiHandler();
            handler = new ContactHandler(handler);
            handler = new RetryHandler(handler);
        }



        #region UserCreate 创建用户
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="department"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ApiResult<String> UserCreate(String name, String mobile, List<Int32> department, String userid = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<UserCreateRequest, UserCreateResponse>("user_create", new UserCreateRequest()
            {
                name = name,
                mobile = mobile,
                department = department,
                userid = userid,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<String> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null && string.IsNullOrEmpty(res.data.userid))
                {
                    rlt.data = res.data.userid;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 

        #region DepartmentList 获取部门列表
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="parentid">父部门id（默认为根部门）</param>
        /// <param name="fetch_child">是否递归部门的全部子部门</param>
        /// <returns></returns>
        public ApiResult<List<Models.Department>> DepartmentList(String parentid = "1", Boolean fetch_child = true)
        {
            var res = GetResponseFromAsyncTask(CallAsync<DepartmentListRequest, DepartmentListResponse>("department_list", new DepartmentListRequest()
            {
                id = parentid,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken(),
                fetch_child = fetch_child ? "true" : "false"
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<List<Models.Department>> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.department;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 

        #region DepartmentCreate 创建部门
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <param name="parentid">父部门id</param>
        /// <param name="order">在父部门中的排序值</param>
        /// <param name="sourceIdentifier">部门标识字段</param>
        /// <returns></returns>
        public ApiResult<Int32> DepartmentCreate(String name, String parentid, String order = "", String sourceIdentifier = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<DepartmentCreateRequest, DepartmentCreateResponse>("department_create", new DepartmentCreateRequest()
            {
                name = name,
                parentid = parentid,
                order = order,
                sourceIdentifier = sourceIdentifier,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<Int32> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.id;
                    rlt.success = res.data.id > 0;
                }
            }
            return rlt;
        }
        #endregion 

        #region GetUserCount 获取企业员工人数
        /// <summary>
        /// 获取企业员工人数
        /// </summary>
        /// <param name="onlyActive">仅含已激活钉钉的人员数量</param>
        /// <returns></returns>
        public ApiResult<Int32> GetUserCount(Boolean onlyActive = false)
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetUserCountRequest, GetUserCountResponse>("get_org_user_count", new GetUserCountRequest()
            {
                onlyActive = onlyActive ? 1 : 0,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<Int32> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.count;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 

        #region GetDeptMemberIds 获取部门用户userid列表
        /// <summary>
        /// 获取部门用户userid列表
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <returns></returns>
        public ApiResult<List<String>> GetDeptMemberIds(String deptId = "1")
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetDeptMemberIdsRequest, GetDeptMemberIdsResponse>("get_dept_nember_ids", new GetDeptMemberIdsRequest()
            {
                deptId = deptId,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<List<String>> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.userIds;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 

        #region GetDeptMemberList 获取部门用户详情
        /// <summary>
        /// 获取部门用户详情
        /// </summary>
        /// <param name="department_id">部门id</param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public ApiResult<List<Models.User>> GetDeptMemberList(String department_id = "1", Int32 size = 100, Int64 offset = 0, String order = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetDeptMemberListRequest, GetDeptMemberListResponse>("get_dept_nember_list", new GetDeptMemberListRequest()
            {
                department_id = department_id,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken(),
                size = size > 0 && size <= 100 ? size : 100,
                offset = offset >= 0 ? offset : 0,
                order = order,
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<List<Models.User>> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.userlist;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion

        #region GetDeptMemberListSimple 获取部门用户
        /// <summary>
        /// 获取部门用户详情
        /// </summary>
        /// <param name="department_id">部门id</param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public ApiResult<List<Models.User>> GetDeptMemberListSimple(String department_id = "1", Int32 size = 100, Int64 offset = 0, String order = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetDeptMemberListRequest, GetDeptMemberListResponse>("get_dept_nember_simple", new GetDeptMemberListRequest()
            {
                department_id = department_id,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken(),
                size = size > 0 && size <= 100 ? size : 100,
                offset = offset >= 0 ? offset : 0,
                order = order,
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<List<Models.User>> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.userlist;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 


    }
}