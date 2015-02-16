using System;
using Android.Widget;
using Android.Views;
using Android.Content;
using Android.Util;
using Android.OS;
using Android.Graphics;
using Android.Runtime;
using System.Collections.Generic;

namespace TopMedicalNews.Android
{
	public class DragGridView:global::Android.Widget.GridView
	{
		public DragGridView (Context context) : base (context)
		{  
			mWindowManager = (IWindowManager)Context.GetSystemService (Context.WindowService).JavaCast<IWindowManager> ();
			mStatusHeight = getStatusHeight (context);
		}

	
		public DragGridView (Context context, IAttributeSet attrs) : base (context, attrs, 0)
		{  
			mWindowManager = (IWindowManager)Context.GetSystemService (Context.WindowService).JavaCast<IWindowManager> ();
			mStatusHeight = getStatusHeight (context);

		
		}

		private global::Android.Views.View mStartDragItemView = null;
		private ImageView mDragImageView;
		private Boolean isDrag = false;

	
		private WindowManagerLayoutParams mWindowLayoutParams;
		private int mDownX;
		private int mDownY;
		private int moveX;
		private int moveY;

		private int mPoint2ItemTop;

		private int mPoint2ItemLeft;

		private int mOffset2Top;

		private int mOffset2Left;
		//
	 
		private IWindowManager mWindowManager;
		private Bitmap mDragBitmap;

		public delegate void SwitchItemEvent (int oldPos, int newPos);

		public event SwitchItemEvent OnSwitchItemEvent;
		//
		public delegate void ClickItemEvent(int pos);
		public event ClickItemEvent OnClickItemEvent;
		//
		private void createDragImage (Bitmap bitmap, int downX, int downY)
		{  
			mWindowLayoutParams = new WindowManagerLayoutParams ();  
			mWindowLayoutParams.Format = Format.Translucent; //图片之外的其他地方透明  
			mWindowLayoutParams.Gravity = GravityFlags.Top | GravityFlags.Left;  
			mWindowLayoutParams.X = downX - mPoint2ItemLeft + mOffset2Left;  
			mWindowLayoutParams.Y = downY - mPoint2ItemTop + mOffset2Top - mStatusHeight;  
			mWindowLayoutParams.Alpha = 0.55f; //透明度  
			mWindowLayoutParams.Width = WindowManagerLayoutParams.WrapContent;    
			mWindowLayoutParams.Height = WindowManagerLayoutParams.WrapContent;    
			mWindowLayoutParams.Flags = WindowManagerFlags.NotTouchable
			| WindowManagerFlags.NotFocusable;

			mDragImageView = new ImageView (this.Context);    
			mDragImageView.SetImageBitmap (bitmap);    
			mWindowManager.AddView (mDragImageView, mWindowLayoutParams); 
			mStartDragItemView.Visibility = ViewStates.Invisible;

		}

		Handler mHandler = new Handler ();
		Action mLongClickRunnable = null;

		public bool DragMode{ get; set; }

		public override bool DispatchTouchEvent (MotionEvent e)
		{
		
			switch (e.Action) {  

			case MotionEventActions.Down:  
				mDownX = (int)e.GetX ();
				mDownY = (int)e.GetY ();
				mDragPosition = PointToPosition (mDownX, mDownY);  
				if (mDragPosition == AdapterView.InvalidPosition) {  
					return base.DispatchTouchEvent (e);  
				}
					//
				mHandler.PostDelayed (mLongClickRunnable = () => {
					if (DragMode == true) {
						isDrag = true; //设置可以拖拽  
						mStartDragItemView.Visibility = ViewStates.Invisible;//(View.INVISIBLE);//隐藏该item  
						//根据我们按下的点显示item镜像  
						createDragImage (mDragBitmap, mDownX, mDownY);  
					}
				}, 200);  

					//
				mStartDragItemView = GetChildAt (mDragPosition - FirstVisiblePosition);

				//
				mPoint2ItemTop = mDownY - mStartDragItemView.Top;  
				mPoint2ItemLeft = mDownX - mStartDragItemView.Left;  
				//
				mOffset2Top = (int)(e.RawY - mDownY);  
				mOffset2Left = (int)(e.RawX - mDownX);  
				//开启mDragItemView绘图缓存  
				mStartDragItemView.DrawingCacheEnabled = true;  
				mDragBitmap = Bitmap.CreateBitmap (mStartDragItemView.DrawingCache);  
				mStartDragItemView.DestroyDrawingCache ();  
				break;  
			case MotionEventActions.Move:  
			
				mDownX = (int)e.GetX ();
				mDownY = (int)e.GetY ();
				if (isDrag && mDragImageView != null)
					onDragItem (mDownX, mDownY);

//					if(!IsTouchInItem(mStartDragItemView, moveX, moveY)){ 
//					System.Diagnostics.Debug.WriteLine ("move4");
//					mHandler.RemoveCallbacks(mLongClickRunnable);  
				 
				break;  
			case MotionEventActions.Up:
				{
					if(mLongClickRunnable != null)
					mHandler.RemoveCallbacks (mLongClickRunnable); 
					if (isDrag) {
						onStopDrag ();  
						isDrag = false;  
					} else {

						if (mDragPosition != AdapterView.InvalidPosition)

						OnClickItemEvent (mDragPosition);
					}

					break; 
				}
			} 

			return  true;  
		
		}

		private Boolean IsTouchInItem (View dragView, int x, int y)
		{  
			if (dragView == null) {  
				return false;  
			}  
			int leftOffset = dragView.Left;  
			int topOffset = dragView.Top;
			if (x < leftOffset || x > leftOffset + dragView.Width) {  
				return false;  
			}  

			if (y < topOffset || y > topOffset + dragView.Height) {  
				return false;  
			}  

			return true;  
		}

		private int mStatusHeight;

		private int mDragPosition;
		//
		private void onDragItem (int moveX, int moveY)
		{  

			mWindowLayoutParams.X = moveX - mPoint2ItemLeft + mOffset2Left;  
			mWindowLayoutParams.Y = moveY - mPoint2ItemTop + mOffset2Top - mStatusHeight;
			;  
			mWindowManager.UpdateViewLayout (mDragImageView, mWindowLayoutParams); //更新镜像的位置  
			onSwapItem (moveX, moveY);  
		}

		private void onStopDrag ()
		{

			View view = GetChildAt (mDragPosition - this.FirstVisiblePosition);  
			if (view != null) {  
				view.Visibility = ViewStates.Visible;
				;
			}
		
			removeDragImage ();  
		}

		private void removeDragImage ()
		{  
			if (mDragImageView != null) {  
				mWindowManager.RemoveView (mDragImageView);  
				mDragImageView = null;  
			}  
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{

			int expandSpec = MeasureSpec.MakeMeasureSpec (
				                 Int32.MaxValue >> 2, MeasureSpecMode.AtMost);
			base.OnMeasure(widthMeasureSpec, expandSpec);

			ViewGroup.LayoutParams parm = this.LayoutParameters;//LayoutParams;
			parm.Height= MeasuredHeight;
			this.LayoutParameters = parm;
		}
		private void onSwapItem (int moveX, int moveY)
		{  
			//获取我们手指移动到的那个item的position  
			var tempPosition = PointToPosition (moveX, moveY);  

			//假如tempPosition 改变了并且tempPosition不等于-1,则进行交换  
			if (tempPosition != mDragPosition && tempPosition != AdapterView.InvalidPosition) { 

				if (OnSwitchItemEvent != null) {  
				
//					System.Diagnostics.Debug.WriteLine ("m:" + mDragPosition + "t:" + tempPosition);
					OnSwitchItemEvent (mDragPosition, tempPosition);

				}
				GetChildAt (tempPosition).Visibility = ViewStates.Invisible;//(View.INVISIBLE);//拖动到了新的item,新的item隐藏掉  
				GetChildAt (mDragPosition).Visibility = ViewStates.Visible;//的item显示出来  
				mDragPosition = tempPosition; 
					 



			}  
		}
	
		//		public override bool OnTouchEvent (MotionEvent e)
		//		{
		//			System.Diagnostics.Debug.WriteLine ("move1");
		//			if( isDrag && mDragImageView != null){
		//				switch(e.Action){
		//				case  MotionEventActions.Move:
		//					System.Diagnostics.Debug.WriteLine ("move2");
		//					moveX = (int)e.GetX ();
		//					moveY = (int)e.GetY ();
		//					//拖动item
		//
		//
		//					break;
		//				case MotionEventActions.Up:
		////					System.Diagnostics.Debug.WriteLine ("up");
		////					onStopDrag();  
		////					isDrag = false;  
		//					break;  
		//				}
		//			}  
		//		
		//			return  false  ;
		//		}

		int getStatusHeight (Context context)
		{  
			int statusHeight = 0;  
			Rect localRect = new Rect ();  
			(Context as MainActivity).Window.DecorView.GetWindowVisibleDisplayFrame (localRect);
			statusHeight = localRect.Top;  
//			if (0 == statusHeight){  
//				  
//				try {  
//					var localClass = Class.ForName("com.android.internal.R$dimen");  
//					Object localObject = localClass.NewInstance();  
//					int i5 = Java.Lang.Integer.ParseInt(localClass.GetField("status_bar_height").Get(localObject).ToString());  
//					statusHeight = context.Resources.GetDimensionPixelSize(i5);  
//				} catch (Exception e) {  
//
//				}   
//			}  
			return statusHeight;  
		}
	}
}

