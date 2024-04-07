namespace Ignite {
    public static class Console {
        public static void Print (params object[]? prompts)
            => Print (Position.Left, prompts);
        public static void Print (in Position position = Position.Left, params object[]? prompts) {
            for (var i = 0; i < prompts!.Length - 1; i++)
                Print (prompts[i], position, true);
            Print (prompts[^1], position, false);
        }

        public static void Printl (params object[]? prompts)
            => Printl (Position.Left, prompts);
        public static void Printl (in Position position = Position.Left, params object[]? prompts) {
            if (prompts == null) {
                Print ("", position, true);
                return;
            }

            foreach (var prompt in prompts)
                Print (prompt, position, true);

        }

        public static T Scan<T> (params object[]? prompts)
            => Scan<T> (Position.Left, prompts);
        public static T Scan<T> (in Position position = Position.Left, params object[]? prompts) {
            Print (in position, prompts);
            return Ignite.Convert.Primitive<T> (System.Console.ReadLine ());
        }

        public enum Position {
            Left = 0,
            Right = 1,
            Centered = 2
        }

        private static void Print (in object? prompt = null, Position position = Position.Left, bool mode = false) {
            if (prompt == null)
                return;

            var split = prompt
                .ToString ()!
                .Split ('\n');

            for (var i = 0; i < split.Length - 1; i++)
                Write (split[i] + "\n");

            Write (split[^1]);

            void Write (string write) {
                var overhang = System.Linq.Enumerable.Count (write.ToCharArray (), char.IsControl);

                var tags = new System.Collections.Generic.Dictionary<string, (string, int)> {
                    { "<bold>", ("\u001b[1m", 4) },
                    { "</bold>", ("\u001b[22m", 5) },

                    { "<faint>", ("\u001b[2m", 4) },
                    { "</faint>", ("\u001b[22m", 5) },

                    { "<italic>", ("\u001b[3m", 4) },
                    { "</italic>", ("\u001b[23m", 5) },

                    { "<underline>", ("\u001b[4m", 4) },
                    { "</underline>", ("\u001b[24m", 5) },

                    { "<blinking>", ("\u001b[5m", 4) },
                    { "</blinking>", ("\u001b[25m", 5) },

                    { "<reverse>", ("\u001b[7m", 4) },
                    { "</reverse>", ("\u001b[27m", 5) },

                    { "<invisible>", ("\u001b[8m", 4) },
                    { "</invisible>", ("\u001b[28m", 5) },

                    { "<strikethrough>", ("\u001b[9m", 4) },
                    { "</strikethrough>", ("\u001b[29m", 5) },

                    { "<foreground-gray>", ("\u001b[90m", 5)},
                    { "<foreground-red>", ("\u001b[91m", 5)},

                    { "<foreground-green>", ("\u001b[92m", 5)},
                    { "<foreground-yellow>", ("\u001b[93m", 5)},

                    { "<foreground-blue>", ("\u001b[94m", 5)},
                    { "<foreground-magenta>", ("\u001b[95m", 5)},

                    { "<foreground-cyan>", ("\u001b[96m", 5)},
                    { "<foreground-white>", ("\u001b[97m", 5)},

                    { "<background-gray>", ("\u001b[100m", 6)},
                    { "<background-red>", ("\u001b[101m", 6)},

                    { "<background-green>", ("\u001b[102m", 6)},
                    { "<background-yellow>", ("\u001b[103m", 6)},

                    { "<background-blue>", ("\u001b[104m", 6)},
                    { "<background-magenta>", ("\u001b[105m", 6)},

                    { "<background-cyan>", ("\u001b[106m", 6)},
                    { "<background-white>", ("\u001b[107m", 6)},

                    { "<foreground-black>", ("\u001b[30m", 5)},
                    { "<background-black>", ("\u001b[40m", 5)},

                    { "</foreground>", ("\u001b[39m", 5)},
                    { "</background>", ("\u001b[49m", 5)}
                };

                while (write.Contains ("<foreground-#") || write.Contains ("<background-#")) {
                    var start = write.IndexOf ("<foreground-#");
                    var background = false;

                    if (start == -1) {
                        start = write.IndexOf ("<background-#");
                        background = true;
                    }

                    var end = write.IndexOf ('>', start);
                    var color = (end - (start + 0xD)) switch {
                        6 => write[(end - 6)..end],
                        3 => write[(end - 3)..end],
                        _ => throw new System.ArgumentException ("Invalid color code length. The color code must be either 3 or 6 characters long.")
                    };

                    var rgb = Parse (color);
                    var sequence = $"\u001b[{(!background ? "38" : "48")};2;{rgb.Item1};{rgb.Item2};{rgb.Item3}m";

                    write = write.Remove (start, end - start + 1).Insert (start, sequence);
                    overhang += sequence.Length;
                }

                foreach (var tag in tags) {
                    while (write.Contains (tag.Key)) {
                        var index = write.IndexOf (tag.Key);
                        write = write[..index] + tag.Value.Item1 + write[(index + tag.Key.Length)..];
                        overhang += tag.Value.Item2;
                    }
                }

                overhang = write.Length - overhang;

                if (mode) {
                    System.Console.WriteLine (position switch {
                        Position.Right => new string (' ', System.Console.WindowWidth - overhang) + write,
                        Position.Centered => new string (' ', (System.Console.WindowWidth - overhang) >> 1) + write,
                        _ => write
                    });
                } else {
                    System.Console.Write (position switch {
                        Position.Right => new string (' ', System.Console.WindowWidth - overhang) + write,
                        Position.Centered => new string (' ', (System.Console.WindowWidth - overhang) >> 1) + write,
                        _ => write
                    });
                }
            }

            static (int, int, int) Parse (string color) {
                color = color.Length == 3
                    ? $"{color[0]}{color[0]}{color[1]}{color[1]}{color[2]}{color[2]}"
                    : color;

                return (
                    int.Parse (color[..2], System.Globalization.NumberStyles.HexNumber),
                    int.Parse (color[2..4], System.Globalization.NumberStyles.HexNumber),
                    int.Parse (color[4..6], System.Globalization.NumberStyles.HexNumber)
                );
            }
        }
    }
}