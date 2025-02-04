using System.Runtime.InteropServices;

public partial class Win32
{
    [LibraryImport("User32.dll")]
    public static partial long SetCursorPos(int x, int y);

    [LibraryImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ClientToScreen(IntPtr hWnd, ref POINT point);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT(int X, int Y)
    {
        public int x = X;
        public int y = Y;
    }
}


public class Program
{
    private static int[] xyPositions = [256, 512, 964];

    public static void Main()
    {
        // Create a Timer object that knows to call our TimerCallback
        // method once every 5000 milliseconds.
        Timer _ = new(TimerCallback, null, 0, 5000);
        // Wait for the user to hit <Enter>
        Console.ReadLine();
    }

    private static void TimerCallback(object? _)
    {
        int position = xyPositions[(DateTime.Now.Second / 5) % xyPositions.Length];
        Win32.POINT p = new(position, position);
        Win32.ClientToScreen(IntPtr.Zero, ref p);
        Win32.SetCursorPos(p.x, p.y);

        Console.WriteLine($"[{DateTime.Now}] Successfully moved mouse! ({p.x}, {p.y})");
    }
}