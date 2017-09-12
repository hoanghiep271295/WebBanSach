$(function () {
    $(".showsach").click(function(e) {
        e.preventDefault();
        var $this = $(this);
        var idshow = $this.attr('id');
        switch (idshow) {
            case 'showdm':
                {
                    $('#danhmucshowfull').show();
                    $('#danhmucshow').hide();
                    break;
                }
            case 'showtl':
                {
                    $('#theloaishowfull').show();
                    $('#theloaishow').hide();
                    break;
                }
            case 'showtg':
                {
                    $('#tacgiashowfull').show();
                    $('#tacgiashow').hide();
                    break;
                }
            case 'shownxb':
                {
                    $('#nxbshowfull').show();
                    $('#nxbshow').hide();
                    break;
                }
        }

    });

});
$(function () {
    $(".lasthide").click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var idshow = $this.attr('id');
        switch (idshow) {
            case 'hidedm':
                {
                    $('#danhmucshowfull').hide();
                    $('#danhmucshow').show();
                    break;
                }
            case 'hidetl':
                {
                    $('#theloaishowfull').hide();
                    $('#theloaishow').show();
                    break;
                }
            case 'hidetg':
                {
                    $('#tacgiashowfull').hide();
                    $('#tacgiashow').show();
                    break;
                }
            case 'hidenxb':
                {
                    $('#nxbshowfull').hide();
                    $('#nxbshow').show();
                    break;
                }
        }

    });

});