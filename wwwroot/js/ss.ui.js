// Signature Instance ctlSignature


    var instanceId = "ctlSignature";
    var objctlSignature = null;

    var isMobile = false;

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        isMobile = true;
    }

function ctlSignatureInit(signModel) {
    //debugger;
        signModel = signModel.replace(/"([^"]+)":/g, '$1:');
        objctlSignature = new SuperSignature(eval('(' + signModel + ')'));

        objctlSignature.Init();

        $("#" + instanceId + "_toolbar").show();
        $("#imgLoader").hide();

        Resize();
    }

    // End


    function saveSignatureAsync(dotNetObjectReference) {
        var imageData = document.getElementById(instanceId).toDataURL("image/png");

        // client side image
        //$("#pngImg").attr("src", imageData);

        //server side save
        //DotNet.invokeMethodAsync('WiiTrakClient', 'SaveSignatureAsync', imageData);

        dotNetObjectReference.invokeMethodAsync('SaveSignatureAsync', imageData);
    }


    // ============================== Code for Resizing  ============================== 

    var resizing = false;

    function Resize() {
        if (resizing) {
            return;
        }

        resizing = true;

        var divResize = $("#divResize");
        var signW = parseInt(divResize.width());
        var signH = "innerHeight" in window ? window.innerHeight : document.documentElement.offsetHeight;

        // adjust the values as required to match your UI
        if (isMobile) {
            signH = signH - 50;
            signW = signW - 10;
            $("#bTitle").hide();
        } else {
            signH = signH - 250;
            signW = signW - 50;
        }

        try {
            ResizeSignature(instanceId, signW, signH);
            setTimeout(function () { ClearSignature(instanceId); }, 100);
        }
        catch (e) {

        }

        divResize.show();
        resizing = false;
    }

    function AttachEvents() {
        //debugger;
        if (isMobile) {

            $(window).on("load orientationchange",
                function (e) {
                    setTimeout(function () { Resize(); }, 500);
                });

        } else {

            $(window).on("load resize",
                function () {
                    setTimeout(function () { Resize(); }, 500);
                });
        }

    }

    AttachEvents();