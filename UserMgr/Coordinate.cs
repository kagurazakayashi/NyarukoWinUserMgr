namespace WinUserMgr
{
    /// <summary>
    /// 表示一個二維座標（行和列）。
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// 獲取或設定座標的行值。
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 獲取或設定座標的列值。
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// 構造一個新的座標例項。
        /// </summary>
        /// <param name="row">行值。</param>
        /// <param name="col">列值。</param>
        public Coordinate(int row, int col)
        {
            Row = row;  // 初始化行值
            Col = col;  // 初始化列值
        }

        /// <summary>
        /// 重寫物件的相等比較方法，判斷兩個座標是否相同。
        /// </summary>
        /// <param name="obj">要比較的物件。</param>
        /// <returns>如果兩個座標相等，返回true；否則返回false。</returns>
        public override bool Equals(object obj)
        {
            // 檢查 obj 是否是 Coordinate 型別
            if (obj is Coordinate)
            {
                Coordinate other = (Coordinate)obj;  // 強制轉換 obj 為 Coordinate 型別
                return Row == other.Row && Col == other.Col;  // 行列值相等則認為座標相等
            }
            return false;  // 如果物件型別不匹配，則返回 false
        }

        /// <summary>
        /// 重寫 GetHashCode 方法，為座標生成唯一的雜湊值。
        /// </summary>
        /// <returns>返回表示當前座標的雜湊值。</returns>
        public override int GetHashCode()
        {
            // 使用質數生成雜湊值，以減少碰撞的機率
            int hash = 17;  // 初始雜湊值
            hash = hash * 23 + Row.GetHashCode();  // 行值對雜湊值的貢獻
            hash = hash * 23 + Col.GetHashCode();  // 列值對雜湊值的貢獻
            return hash;  // 返回計算後的雜湊值
        }
    }
}
