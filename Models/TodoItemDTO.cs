using AutoMapperSample.Adapter.Models;

namespace AutoMapperSample.Models
{
    public class TodoItemDTO 
    {
        /// <summary>
        /// 代辦事項代碼
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否有子項目
        /// </summary>
        public int IsSub { get; set; }

        /// <summary>
        /// 代辦事項標籤
        /// </summary>
        public List<Tag> Tags { get; set; } = [];

        /// <summary>
        /// 代辦事項名稱
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 代辦事項-時間設定
        /// </summary>
        public TodoDateTimeSetting TodoDateTimeSetting { get; set; } = new TodoDateTimeSetting();

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最後異動時間
        /// </summary>
        public DateTime? EditTime { get; set; }

        /// <summary>
        /// 子項目
        /// </summary>
        public List<Todo> SubTodos { get; set; } = [];
    }
}
