WinPage = function() {
};

WinPage.prototype.Open = function(title, width, height, url, data) {
    url = url + "?id=" + data.ID;
    var win = new Ext.Window({
        title: title,
        layout: 'fit',    //设置布局模式为fit，能让frame自适应窗体大小
        modal: true,    //打开遮罩层

        height: height,    //初始高度
        width: width,  //初始宽度
        frame: true,    //去除窗体的panel框架
        html: '<iframe frameborder=0 width="100%" height="100%" allowtransparency="true" scrolling=auto src="' + url + '"></iframe>'
    });
    //win.setSize(width, height);
    win.show();
};


var ExtWindow = new WinPage();