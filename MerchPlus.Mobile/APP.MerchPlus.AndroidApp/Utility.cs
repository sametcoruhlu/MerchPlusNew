using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Hardware.Lights;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace APP.MerchPlus
{
    public static class Utility
    {
        public static void SetListViewHeightBasedOnChildren(ListView listView)
        {
            var listAdapter = listView.Adapter;
            if (listAdapter == null)
            {
                return;
            }

            int totalHeight = 0;
            for (int i = 0; i < listAdapter.Count; i++)
            {
                var listItem = listAdapter.GetView(i, null, listView);

                // Ölçüm işlemi
                listItem.Measure(
                    View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.AtMost),
                    View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                totalHeight += listItem.MeasuredHeight;
            }

            // Yükseklik ayarı
            var layoutParams = listView.LayoutParameters;
            layoutParams.Height = totalHeight + (listView.DividerHeight * (listAdapter.Count - 1));
            listView.LayoutParameters = layoutParams;
            listView.RequestLayout();
        }
    }


}
