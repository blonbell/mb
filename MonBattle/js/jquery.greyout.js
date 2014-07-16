(function ($) {
    $.fn.duplicate = function () {
        this.each(function () {
            var width = $(this).width();
            var height = $(this).height();

            //wrap and store the images inside
            $(this).wrap('<div class="greyOutContainer">');
            $(this).parent('.greyOutContainer').
                css({ 'position': 'relative', "width": width, "height": height });

            // creates a grayed out clone

            $(this).css({ "position": "absolute", 'z-index': 1, "opacity": "1", "top": 0, "left": 0 });
            var greyClone = $(this).clone();
            greyClone.addClass('greyClone');
            greyClone.css({ "position": "absolute", 'z-index': 0, "opacity": "1", "top": 0, "left": 0 });
            greyClone.src = this.src;
            greyClone.insertAfter($(this));

            $(this).addClass('colorOriginal');
        });
    }

    $.fn.greyout = function () {
        //start img manipulation for each selected img tag
        this.each(function () {
            this.src = grayscale(this.src);
        });

        /*
         * Returns the grayscale clone of the src. Type is ImageUrl.
         */
        function grayscale(src) {
            var canvas = document.createElement('canvas');
            var context = canvas.getContext("2d");
            var image = new Image();
            image.src = src;
            canvas.width = image.width;
            canvas.height = image.height;
            context.drawImage(image, 0, 0);

            var imgPixels = context.getImageData(0, 0, canvas.width, canvas.height);
            for (var y = 0; y < imgPixels.height; y++) {
                for (var x = 0; x < imgPixels.width; x++) {
                    var i = (y * 4) * imgPixels.width + x * 4;
                    var avg = (imgPixels.data[i] + imgPixels.data[i + 1] + imgPixels.data[i + 2]) / 3;
                    imgPixels.data[i] = avg;
                    imgPixels.data[i + 1] = avg;
                    imgPixels.data[i + 2] = avg;
                }
            }

            context.putImageData(imgPixels, 0, 0, 0, 0, imgPixels.width, imgPixels.height);
            return canvas.toDataURL();
        }
    }
})( jQuery );