using System.IO;
using System.Linq;
using System.Text;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Commons.Helpers
{
    public static class PathHelper
    {
        #region Path name validator
        public static bool IsEmptyPathName( IPath path )
        {
            var p = path.Path;
            return string.IsNullOrEmpty( p );
        }
        #endregion

        #region Path Combine
        public static IPath Combine( char pathSeparator, IPath a, params IPath[] paths )
        {
            if( !paths.Any() )
            {
                return a;
            }

            var last = paths.Last();

            var sb = new StringBuilder();
            sb.Append( a.Path );

            foreach( var x in paths )
            {
                sb.Append( pathSeparator ).Append( x.Path );
            }

            if( last.IsDirectory )
            {
                return new DirectoryPath( sb.ToString() );
            }

            return new FilePath( sb.ToString() );
        }

        public static IPath Combine( IPath a, params IPath[] paths )
            => Combine( Path.DirectorySeparatorChar, a, paths );
        #endregion
    }
}
