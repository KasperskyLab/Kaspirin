// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 3/23/2021.
// Scope of modification:
//   - Code adaptation to project requirements.

using System;
using System.IO;
using System.Reflection;
using System.ComponentModel;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpVectors.Runtime
{
    public class EmbeddedBitmapSource : BitmapSource
    {
        #region Private Fields

        private string _mimeType;

        private BitmapImage  _bitmap;
        private MemoryStream _stream;

        #endregion Fields

        #region Constructors and Destructor

        public EmbeddedBitmapSource()
        {
            _mimeType = "image/png";

            // Set the _useVirtuals private fields of BitmapSource to true. 
            // otherwise you will not be able to call BitmapSource methods.
            FieldInfo field = typeof(BitmapSource).GetField("_useVirtuals", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                throw new InvalidOperationException("FieldInfo for _useVirtuals is null");

            field.SetValue(this, true);
        }

        public EmbeddedBitmapSource(MemoryStream stream)
            : this()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _stream = stream;
            // Associated this class with source.
            this.BeginInit();

            _bitmap = new BitmapImage();

            _bitmap.BeginInit();
            _bitmap.StreamSource = _stream;
            _bitmap.EndInit();

            this.InitWicInfo(_bitmap);
            this.EndInit();
        }

        public EmbeddedBitmapSource(MemoryStream stream, BitmapImage image)
            : this()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _stream = stream;
            // Associated this class with source.
            this.BeginInit();

            if (image == null)
            {
                _bitmap = new BitmapImage();

                _bitmap.BeginInit();
                _bitmap.StreamSource = _stream;
                _bitmap.EndInit();
            }
            else
            {
                _bitmap = image;
            }

            this.InitWicInfo(_bitmap);
            this.EndInit();
        }

        #endregion Constructors

        #region Public Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public EmbeddedBitmapData Data
        {
            get
            {
                return new EmbeddedBitmapData(_stream);
            }
            set
            {
                BeginInit();

                _stream = value.Stream;

                _bitmap = new BitmapImage();
                _bitmap.BeginInit();
                _bitmap.StreamSource = _stream;
                _bitmap.EndInit();

                InitWicInfo(_bitmap);
                EndInit();
            }
        }

        public override double DpiX
        {
            get
            {
                EnsureStream();
                return base.DpiX;
            }
        }

        public override double DpiY
        {
            get
            {
                EnsureStream();
                return base.DpiY;
            }
        }

        public override PixelFormat Format
        {
            get
            {
                EnsureStream();
                return base.Format;
            }
        }

        public override BitmapPalette Palette
        {
            get
            {
                EnsureStream();
                return base.Palette;
            }
        }

        public override int PixelWidth
        {
            get
            {
                EnsureStream();
                return base.PixelWidth;
            }
        }

        public override int PixelHeight
        {
            get
            {
                EnsureStream();
                return base.PixelHeight;
            }
        }

        public string MimeType 
        {
            get 
            {
                return _mimeType;
            }
            set
            {
                if (value != null && value.Length != 0) 
                { 
                    _mimeType = value;
                }
            }
        }

        #endregion Properties

        #region Public Methods

        public override void CopyPixels(Int32Rect sourceRect, IntPtr buffer, 
            int bufferSize, int stride)
        {
            EnsureStream();
            base.CopyPixels(sourceRect, buffer, bufferSize, stride);
        }

        #endregion

        #region Protected Methods

        protected override void CloneCore(Freezable sourceFreezable)
        {
            EmbeddedBitmapSource cloneSource = (EmbeddedBitmapSource)sourceFreezable;
            CopyFrom(cloneSource);
            //base.CloneCore( sourceFreezable );
        }

        protected override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            EmbeddedBitmapSource cloneSource = (EmbeddedBitmapSource)sourceFreezable;
            CopyFrom(cloneSource);
            //base.CloneCurrentValueCore( sourceFreezable );
        }

        protected override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            EmbeddedBitmapSource cloneSource = (EmbeddedBitmapSource)sourceFreezable;
            CopyFrom(cloneSource);
            //base.GetAsFrozenCore( sourceFreezable );
        }

        protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            EmbeddedBitmapSource cloneSource = (EmbeddedBitmapSource)sourceFreezable;
            CopyFrom(cloneSource);
            //base.GetCurrentValueAsFrozenCore( sourceFreezable );
        }

        protected override Freezable CreateInstanceCore()
        {
            return new EmbeddedBitmapSource();
        }

        #endregion Override Methods

        #region Private Methods

        /// <summary>
        /// Call BeginInit every time the WICSourceHandle is going to be change.
        /// again this methods is not exposed and reflection is needed.
        /// </summary>
        private void BeginInit()
        {
            FieldInfo field = typeof(BitmapSource).GetField(
                "_bitmapInit", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                throw new InvalidOperationException("FieldInfo for _bitmapInit is null");

            MethodInfo beginInit = field.FieldType.GetMethod(
                "BeginInit", BindingFlags.Public | BindingFlags.Instance);

            if (beginInit == null)
                throw new InvalidOperationException("MethodInfo for BeginInit is null");

            beginInit.Invoke(field.GetValue(this), null);
        }

        /// <summary>
        /// Call EndInit after the WICSourceHandle was changed and after using BeginInit.
        /// again this methods is not exposed and reflection is needed.
        /// </summary>
        private void EndInit()
        {
            FieldInfo field = typeof(BitmapSource).GetField(
                "_bitmapInit", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                throw new InvalidOperationException("FieldInfo for _bitmapInit is null");

            MethodInfo endInit = field.FieldType.GetMethod(
                "EndInit", BindingFlags.Public | BindingFlags.Instance);

            if (endInit == null)
                throw new InvalidOperationException("MethodInfo for EndInit is null");

            endInit.Invoke(field.GetValue(this), null);
        }

        /// <summary>
        /// Set the WicSourceHandle property with the source associated with this class.
        /// again this methods is not exposed and reflection is needed.
        /// </summary>
        /// <param name="source"></param>
        private void InitWicInfo(BitmapSource source)
        {
            //
            // Use reflection to get the private property WicSourceHandle Get and Set methods.
            PropertyInfo wicSourceHandle = typeof(BitmapSource).GetProperty(
                "WicSourceHandle", BindingFlags.NonPublic | BindingFlags.Instance);

            if (wicSourceHandle == null)
                throw new InvalidOperationException("PropertyInfo for WicSourceHandle is null");

            MethodInfo wicSourceHandleGetMethod = wicSourceHandle.GetGetMethod(true);
            MethodInfo wicSourceHandleSetMethod = wicSourceHandle.GetSetMethod(true);

            if (wicSourceHandleGetMethod == null)
                throw new InvalidOperationException("wicSourceHandleGetMethod is null");

            if (wicSourceHandleSetMethod == null)
                throw new InvalidOperationException("wicSourceHandleSetMethod is null");

            //
            // Call the Get method of the WicSourceHandle of source.
            object wicHandle = wicSourceHandleGetMethod.Invoke(source, null);
            //
            // Call the Set method of the WicSourceHandle of this with the value from source.
            wicSourceHandleSetMethod.Invoke(this, new object[] { wicHandle });
        }

        private void CopyFrom(EmbeddedBitmapSource source)
        {
            this.BeginInit();

            _stream = source._stream;
            _bitmap = source._bitmap;

            this.InitWicInfo(_bitmap);
            this.EndInit();
        }

        /// <summary>
        /// In the designer Data is not set. To prevent exceptions when displaying in the Designer, add a dummy bitmap.
        /// </summary>
        private void EnsureStream()
        {
            if (_stream == null)
            {
                BitmapSource dummyBitmap = BitmapSource.Create(1, 1, 96.0, 96.0,
                    PixelFormats.Pbgra32, null, new byte[] { 0, 0, 0, 0 }, 4);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(dummyBitmap));
                MemoryStream stream = new MemoryStream();
                encoder.Save(stream);
                this.Data = new EmbeddedBitmapData(stream);
            }
        }

        #endregion Methods
    }
}
