<template>
    <div>
        <Card>
            <p slot="title">{{'数据管理'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                        
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create' v-if="persBtn.create" >{{'添加' | l}}</DropdownItem>
                    <DropdownItem name='DeleteAll' v-if="persBtn.create" >{{'删除所有' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            <Table  :columns="columns" height="400" border :data="reportresults"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                       <Form ref="reportresultForm" label-position="top" :rules="reportresultRule" :model="editreportresult">
                            <FormItem :label="L('流水号')" prop="description">
                                <Input v-model="editreportresult.description" disabled  ></Input>
                            </FormItem>
                            <FormItem :label="L('名称')" prop="name">
                                <Input v-model="editreportresult.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('下载')" prop="url">
                                <Input v-model="editreportresult.url" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                                              
                        </Form>
                            
            </div>
            <div slot="footer">
                <Button @click="showModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="reportresultForm" label-position="top" :rules="reportresultRule" :model="editreportresult">
                            <FormItem :label="L('流水号')" prop="description">
                                <Input v-model="editreportresult.description" disabled  ></Input>
                            </FormItem>
                            <FormItem :label="L('名称')" prop="name">
                                <Input v-model="editreportresult.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('下载')" prop="url">
                                <Input v-model="editreportresult.url" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                           
                            
                                              
                        </Form>
            </div>
            <div slot="footer">
                <Button @click="showEditModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script>
export default {
    methods:{
        create(){
            this.editreportresult={};  
            this.editreportresult.description=abp.randomNumber();
            this.showModal=true;
        },
        async save(){
            if(!!this.editreportresult.id){
                this.$refs.reportresultForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'reportresult/update',
                            data:this.editreportresult
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newreportresultForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'reportresult/create',
                            data:this.editreportresult
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('reportresult/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('reportresult/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'reportresult/getAll'
            })
        },
        
        onselectionchange(row){
            this.selectedData=row;
        },
        download(url){

             this.$store.dispatch({
                    type:'reportresult/download',
                    data:{
                        file:url
                    }
                }).then(function (response) {
                    abp.downloadfile(response.data,url);
                }).catch(function (error) {
                    console.log(error);
                    
                });
        },
        reportdownload(url){

             this.$store.dispatch({
                    type:'reportresult/reportdownload',
                    data:{
                        numberNo:url
                    }
                }).then(function (response) {
                    abp.downloadfile(response.data,url+'.xlsx');
                }).catch(function (error) {
                    console.log(error);
                    
                });
        },
        handleClickActionsDropdown(name){
            if(name==='Create'){
                this.create();
            }else if(name==='Refresh'){
                this.getpage();
                this.$Message.success('刷新成功！');
            }else if(name=='DeleteAll'){
                var $this=this;
                this.$store.dispatch({
                    type:'reportresult/deleteAll'
                }).then(function (response) {
                    $this.getpage();
                    $this.$Message.success('刷新成功！');
                }).catch(function (error) {
                    console.log(error);
                    
                });
            }
        }
    },
    data(){
        return{
            editreportresult:{},
             persBtn:{
                create:true,//abp.auth.isGranted('Pages.reportresults.Create'),
                update:true,//abp.auth.isGranted('Pages.reportresults.Update'),
                delete:true,//abp.auth.isGranted('Pages.reportresults.Delete'),
            },
            showModal:false,
            showEditModal:false,
            selectedData:[],
            newreportresultRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },            
            reportresultRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },
            columns:[
            
                    {
                        type: 'index',
                        width: 60,
                        align: 'center'
                    },{
                title:this.L('流水号'),
                key:'description'
            },
            {
                title:this.L('名称'),
                key:'name'
            },{
                title:this.L('下载'),
                key:'url',
                render:(h,params)=>{
                    var btns=[];
                     var d=h('Button',{
                                            props:{
                                                type:'primary',
                                                size:'small'
                                            },
                                            style:{
                                                marginRight:'5px'
                                            },
                                            on:{
                                                click:()=>{
                                                    this.download(this.reportresults[params.index].url);
                                                }
                                            }
                                        },this.L('下载'));
                                btns.push(d);
                                var d1=h('Button',{
                                            props:{
                                                type:'primary',
                                                size:'small'
                                            },
                                            style:{
                                                marginRight:'5px'
                                            },
                                            on:{
                                                click:()=>{
                                                    this.reportdownload(this.reportresults[params.index].description);
                                                }
                                            }
                                        },this.L('重新生成'));
                                btns.push(d1);
                     return h('div',btns);  
                }
            }]
        }
    },
    computed:{
        reportresults(){
            return this.$store.state.reportresult.reportresults;
        },
        
        totalCount(){
            return this.$store.state.reportresult.totalCount;
        },
        currentPage(){
            return this.$store.state.reportresult.currentPage;
        },
        pageSize(){
            return this.$store.state.reportresult.pageSize;
        }
    },
    async created(){
        this.getpage();
        
    }
}
</script>



