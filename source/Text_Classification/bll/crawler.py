from bll import load_page
import re
import requests
from bll.bll_text_classification import predict, convert_label_to_labelID
from bll.preprocessor import text_preprocess

POSTS = '/posts/'
PAGE_URL = 'https://www.facebook.com/viettan' + POSTS
PAGE_ID = 'viettan'
SCROLL_DOWN = 2


def get_child_attribute(element, selector, attr):
    try:
        element = element.find_element_by_css_selector(selector)
        return str(element.get_attribute(attr))
    except:
        return ''


load_page.start(PAGE_URL, SCROLL_DOWN)
driver = load_page.driver

listJsonPosts = []
listHtmlPosts = driver.find_elements_by_css_selector('[class="_427x"] .userContentWrapper')
print('Start crawling', len(listHtmlPosts), 'posts...')

for post in listHtmlPosts:
    post_url = get_child_attribute(post, '._5pcq', 'href').split('?')[0]
    post_id = re.findall('\d+', post_url)[-1]
    time = get_child_attribute(post, 'abbr', 'title')
    post_text = get_child_attribute(post, '.userContent', 'textContent')
    total_react = get_child_attribute(post, '[data-testid="UFI2ReactionsCount/root"] ._81hb', 'innerText')
    total_shares = get_child_attribute(post, '._3rwx', 'innerText')
    total_cmts = get_child_attribute(post, '._3hg-', 'innerText')

    # get number in comment and shares
    # temp = [int(word) for word in total_react.split() if word.isdigit()]
    # if len(temp) == 1:
    #     total_react = temp[0] * 1000
    # elif len(temp) == 2:
    #     total_react = temp[0] * 1000 + temp[1] * 100
    temp = [int(word) for word in total_shares.split() if word.isdigit()]
    if len(temp) > 0:
        total_shares = temp[0]
    temp = [int(word) for word in total_cmts.split() if word.isdigit()]
    if len(temp) > 0:
        total_cmts = temp[0]

    listJsonPosts.append({
        'PostUrl': post_url,
        'PostID': post_id,
        'Time': time,
        'PostContent': post_text,
        'TotalLikes': total_react,
        'TotalComment': total_cmts,
        'TotalShare': total_shares,
        'NewsLabelID': convert_label_to_labelID(predict(text_preprocess(post_text)))
    })

load_page.stop_and_save('../data/facebook_post_crawled.json', listJsonPosts)

url = 'https://localhost:44347/api/Home/AddNewPost'
response = requests.post(url, json=listJsonPosts, verify=False)
print('Status code: ', response.status_code)
print(response.text)
