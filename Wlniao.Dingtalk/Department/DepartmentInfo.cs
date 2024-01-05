using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wlniao.Dingtalk.Department
{
    public class DepartmentInfo
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 父部门ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 部门标识字段。第三方企业应用不返回该参数。
        /// </summary>
        public string SourceIdentifier { get; set; }
        /// <summary>
        /// 是否同步创建一个关联此部门的企业群
        /// </summary>
        public bool create_dept_group { get; set; }
        /// <summary>
        /// 当部门群已经创建后，是否有新人加入部门会自动加入该群
        /// </summary>
        public bool auto_add_user { get; set; }


        /// <summary>
        /// 根据部门ID获取指定部门详情
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="dept_id"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static ApiResult<DepartmentInfo> Get(Context ctx, String dept_id, String language = "zh_CN")
        {
            var response = ctx.Request<Dictionary<String, Object>>(Settings.DingtalkOApi, "/topapi/v2/department/get", new { dept_id, language }, 1);
            var result = new ApiResult<DepartmentInfo> { code = response.code, message = response.message };
            if (response.success)
            {
                var item = response.data;
                result.data = new DepartmentInfo
                {
                    Name = item.GetString("name"),
                    DeptId = item.GetString("dept_id"),
                    ParentId = item.GetString("parent_id"),
                    SourceIdentifier = item.GetString("source_identifier"),
                };
                result.success = true;
            }
            return result;
        }

        /// <summary>
        /// 获取下一级部门基础信息
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="dept_id"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static ApiResult<List<DepartmentInfo>> ListSub(Context ctx, String dept_id = "1", String language = "zh_CN")
        {
            var response = ctx.Request<List<Dictionary<String, Object>>>(Settings.DingtalkOApi, "/topapi/v2/department/listsub", new { dept_id, language }, 1);
            var result = new ApiResult<List<DepartmentInfo>> { code = response.code, message = response.message, data = new List<DepartmentInfo>() };
            if (response.success)
            {
                foreach (var item in response.data)
                {
                    result.data.Add(new DepartmentInfo
                    {
                        Name = item.GetString("name"),
                        DeptId = item.GetString("dept_id"),
                        ParentId = item.GetString("parent_id")
                    });
                }
                result.success = true;
            }
            return result;
        }

    }
}