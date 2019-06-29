<style lang="less">
    @import '../../styles/common.less';
</style>
<template>
    <div >
        <Card>
            <p slot="title">模板管理</p>
            <ButtonGroup slot="extra" >
                <Button type="ghost" icon="refresh" @click="refresh">刷新</Button>
                <Button type="ghost" icon="plus"  @click="create" >添加</Button>
                <Button type="ghost" icon="edit"  @click="update">编辑</Button>
                <Button type="ghost" icon="trash-a"  @click="deletes">删除</Button>
             </ButtonGroup>
             <Row>
                <Col span="6">
                    <Card style="min-height:480px;max-height:480px;overflow-y:auto;"> 
                        <p slot="title">
                            文档
                        </p>
                       <Tree :data="documents" :load-data="loadData" @on-select-change="onselectchange"  ></Tree>
                    </Card>
                </Col>
                <Col span="18" class="padding-left-10">
                    <Card style="min-height:480px;"> 
                        <p slot="title">
                            详情
                        </p>
                         <div class="padding10" v-if="showDetails">
                        <table class="hovertable" :model="detailsData">
                            <tr ><td colspan="2" class="table_title">信息详情</td></tr>
                            
                            <tr ><td class="td1">名称：</td><td class="td2">{{detailsData.name}}</td></tr>
                            <tr ><td class="td1">编码：</td><td class="td2">{{detailsData.code}}</td></tr>
                            <tr ><td class="td1">物化路径：</td><td class="td2">{{detailsData.pathName}}</td></tr>
                            
                            <tr ><td class="td1">项目编码：</td><td class="td2">{{detailsData.dataDefine.projectCode}}</td></tr>
                            <tr ><td class="td1">文件类型编码：</td><td class="td2">{{detailsData.fileTypeCode}}</td></tr>
                     <tr><td class="td1">排序：</td><td class="td2">{{detailsData.dateOrderby}}</td></tr>
                     <tr><td class="td1">是否子叶子：</td><td class="td2">{{detailsData.isLeaf?"是":"否"}}</td></tr>

                            <tr ><td class="td1">模板路径：</td><td class="td2">
<Upload :action="uploadurl" 
        :on-success="handleSuccess"
        :show-upload-list="false"
>
        <Button type="primary" size="small" icon="ios-cloud-upload-outline">上传</Button>
    </Upload>
                            {{detailsData.description}}
                            </td></tr>
                            
                        </table>
                    </div>
                    </Card>
                </Col>
            </Row>
        </Card>
    </div>
</template>

<script>
import Cookies from 'js-cookie';
export default {
    name: 'templateManager',
    data () {
        return {
            detailsData:{},
            selectData:{},
            showDetails:false
        }
    },
    methods: {
        create(){
            
            
        },
        update(){
                
        },  
        deletes(){
            
           
        }, 
        refresh(){
           this.getpage();
            this.$Message.success('刷新成功');
        },   
        onselectchange(data){
           this.showDetails=true;
           this.selectData=data.pop();
           this.detailsData=this.selectData.data;
           
        },
        async loadData (item, callback) {
                var data= await this.$store.dispatch({
                    type:'document/getdocumentFile',
                    data:item.id
                });
                callback(data);
            },
        async getpage(){
            await this.$store.dispatch({
                type:'document/getAll'
            });
            
        },
         handleSuccess (res, file) {
            this.detailsData.description=file.response.result.filenames[0];
                this.$store.dispatch({
                            type:'document/update',
                            data:this.detailsData
                })
            },
            handleFormatError (file) {
                this.$Notice.warning({
                    title: 'The file format is incorrect',
                    desc: 'File format of ' + file.name + ' is incorrect, please select jpg or png.'
                });
            },
            handleMaxSize (file) {
                this.$Notice.warning({
                    title: 'Exceeding file size limit',
                    desc: 'File  ' + file.name + ' is too large, no more than 2M.'
                });
            },
            handleBeforeUpload () {
                const check = this.uploadList.length < 5;
                if (!check) {
                    this.$Notice.warning({
                        title: 'Up to five pictures can be uploaded.'
                    });
                }
                return check;
            }
         
    },
     
    computed:{
        
        documents(){
            return this.$store.state.document.documentFiles;
        },
        uploadurl(){
            return this.$store.state.document.uploadurl;
        }
      


        
    },
    async created(){
        this.getpage();
       
    }
};
</script>
