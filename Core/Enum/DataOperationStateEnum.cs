namespace CSRTMISYC.Core.Enum
{
    /// <summary>
    /// 数据操作状态
    /// </summary>
    public enum DataOperationStateEnum
    {
        新增成功 = 1,
        新增失败 = 2,
        更新成功 = 3,
        更新失败 = 4,
        删除成功 = 5,
        删除失败 = 6,
        记录存在 = 7,
        记录不存在 = 8
    }

    public enum DataOperationType
    {
        新增 = 1,
        修改 = 2,
        删除 = 3,
        默认 = 4
    }
}
