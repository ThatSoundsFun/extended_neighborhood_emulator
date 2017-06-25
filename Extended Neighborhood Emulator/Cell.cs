
//Represents the normal on and off cells.
enum Cell { OFF = 0, ON = 1 }
static class CellExtension {
    private const int OffCountOffset = 1;
    private const int OnCountOffset = 10;

    //Changes the cell to its equivalent group of count-states.
    public static string ToCountGroup(this Cell cell, int variant) {
        if (cell == Cell.OFF) {
            return "0c" + variant.ToString();
        }
        return "1c" + variant.ToString();
    }

    //Changes the cell to a specific count-state.
    public static string ToCountState(this Cell cell, int count) {
        if (cell == Cell.OFF) {
            if (count == 0) {
                return "0";
            }
            return (count + OffCountOffset).ToString();
        }
        return (count + OnCountOffset).ToString();
    }
}   