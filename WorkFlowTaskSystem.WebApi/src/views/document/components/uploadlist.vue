<style lang="less">
    @import '../../../styles/common.less';
    @import 'upload.less';
</style>


<template>
    <div>
     
                        
         <p >{{title}}</p>
            <Row >
            <Col span="4">
                <Card>
                                        <Upload
                                            ref="upload"
                                            :show-upload-list="false"
                                            :default-file-list="defaultList"
                                            :on-success="handleSuccess2"
                                            :max-size="20480"
                                            :format="defaultformat"
                                            :on-format-error="handleFormatError2"
                                            :on-exceeded-size="handleMaxSize"
                                            :on-error="handleError"
                                            :before-upload="handleBeforeUpload2"
                                            :multiple="multiple"
                                            type="drag"
                                            :data="other"
                                            :action="uploadUrl"
                                            style="display: inline-block;width:78px;">
                                            <div style="width:78px;height:78px;line-height: 78px;">
                                                <Icon type="camera" size="20"></Icon>
                                            </div>
                                        </Upload>
                                        <Modal :title="realname" v-model="visible">
                                        <img :src="imgurl" v-if="visible" style="width: 100%">
                                        </Modal>
                                    </Card>
                                </Col>
                                <Col span="20" class="padding-left-10">
                                    <Card style="min-height:120px">
                                        <div class="height-280px">

                                            <div class="admin-upload-list " v-for="item in uploadList" :key="item.url">
                                                <template v-if="item.status === 'finished'">
                                                    <img :src="item.url">
                                                    <div class="admin-upload-list-cover">
                                                        <Icon type="ios-eye-outline" @click.native="handleView(item.url,item.realname)"></Icon>
                                                        <Icon type="ios-download-outline" @click.native="handleDownload(item)"></Icon>
                                                        <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
                                                        
                                                        
                                                    </div>
                                                </template>
                                                <template v-else>
                                                    <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
                                                </template>
                                            </div>
                                        </div>
                                    </Card>
                                </Col>
                            </Row>
                        </div>
    </div>
</template>

<script>
export default {
    name: 'file-upload',
    props: {
        data:Array,
        format:{
            type:Array,
            default:['jpg','jpeg','png']
        },
        multiple: {
            type: Boolean,
            default: false
        },
        title: {
            type: String,
            default: '',
        },
        size:{ 
            type: Number,
            default: 1,
        },
        methods:{
            type: String,
            default: 'upload',
        },
        randomnumber:{
            type: String,
            default: 'string',
        }
        
    },
    data () {
        return {
            defaultList: this.data,
            defaultformat:this.format,
            imgurl: '',
            realname:'',
            other:{methods:this.methods,randomnumber:this.randomnumber},
            visible: false,
            uploadUrl:this.$store.state.filemanager.uploadUrl,
            downloadUrl:this.$store.state.filemanager.downloadUrl,
            uploadList:[]
        };
    },
    methods: {
        handleFormatError (file) {
            this.$Notice.warning({
                title: '文件格式不正确',
                desc: '文件 ' + file.name + ' 格式不正确，请选择图片文件。'
            });
        },
        handleBeforeUpload (file) {
            this.$Notice.warning({
                title: '文件准备上传',
                desc: '文件 ' + file.name + ' 准备上传。'
            });
        },
        handleProgress (event, file) {
            this.$Notice.info({
                title: '文件正在上传',
                desc: '文件 ' + file.name + ' 正在上传。'
            });
        },
        handleSuccess (evnet, file) {
            console.log(file.name);
            this.$Notice.success({
                title: '文件上传成功',
                desc: '文件 ' + file.name + ' 上传成功。'
            });
        },
        handleError (event, file) {
            this.$Notice.error({
                title: '文件上传成功',
                desc: '文件 ' + file.result.name + ' 上传失败。',
                duration:0,
            });
        },
        handleView (url,realname) {
            this.imgurl = url;
            this.realname=realname;
            this.visible = true;
        },
        handleRemove (file) {
            // 从 upload 实例删除数据
            const fileList = this.$refs.upload.fileList;
            this.$refs.upload.fileList.splice(fileList.indexOf(file), 1);
            this.$store.dispatch({
                            type:'filemanager/deleteFile',
                            data:file.name
                        })
        },
        handleDownload (file) {
            this.$store.dispatch({
                    type:'filemanager/downloadFile',
                    data:file.name
                }).then(function (response) {
                    console.log(response);
                    abp.downloadfile(response.data,file.realname);
                }).catch(function (error) {
                    console.log(error);
                    
                });
        },
        handleSuccess2 (res, file) {
            // 因为上传过程为实例，这里模拟添加 url
            //file.url = 'https://o5wwk8baw.qnssl.com/7eb99afb9d5f317c912f08b5212fd69a/avatar';
            file.url=this.downloadUrl+"a7aca5a0-0543-46d8-9860-ef8799a9bf1b.jpg";
            file.name = res.result.filenames[0];
            file.realname = res.result.realnames[0];
        },
        handleFormatError2 (file) {
            var f=this.format.join(',');
            this.$Notice.warning({
                title: '文件格式不正确',
                desc: '文件 ' + file.name + ' 格式不正确，请上传 '+f+' 格式的文件或图片。'
            });
        },
        handleMaxSize (file) {
            this.$Notice.warning({
                title: '超出文件大小限制',
                desc: '文件 ' + file.name + ' 太大，不能超过 20M。'
            });
        },
        handleBeforeUpload2 () {
            const check = this.uploadList.length < this.size;
            if (!check) {
                this.$Notice.warning({
                    title: '最多只能上传 '+this.size+' 个文件。'
                });
            }
            return check;
        }
    },
    mounted () {
        this.uploadList = this.$refs.upload.fileList;
    },

    watch: {
        uploadList(val){
            this.$emit("on-uploadlist-change",val);//组件内对uploadlist变更后向外部发送事件通知
        },
        
        randomnumber(val){
            this.other.randomnumber=val;
        }

    }
};
</script>
